using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.Interfaces;


namespace HP.Manager.Implementation
{
    public class CalculaAtrasosExtraFaltasManager(IMarcacaoRepository _MarcacaoRepository, IDiaApontamentoRepository _DiaApontamentoRepository, IHorarioRepository _horarioRepository) : ICalculaAtrasosExtraFaltasManager
    {
        public async Task CalculaDias(DateTimeOffset dataInicio, DateTimeOffset dataFim, Pessoa pessoa, CancellationToken cancellationToken)
        {
            var inicioDateOnly = DateOnly.FromDateTime(dataInicio.DateTime);
            var fimDateOnly = DateOnly.FromDateTime(dataFim.DateTime);

            var diaApontamentos = await _DiaApontamentoRepository.ObterPorPessoaEPeriodoAsync(pessoa.Id, inicioDateOnly, fimDateOnly, cancellationToken);
            var marcacoes = await _MarcacaoRepository.ObterPorCpfEPeriodoAsync(pessoa.Cpf, dataInicio, dataFim, cancellationToken);

            var horarioPessoa = pessoa.Horarios?.FirstOrDefault(x => x.DataFim is null);
            if (horarioPessoa is null)
                throw new InvalidOperationException($"Nenhum horário ativo encontrado para a pessoa ID: {pessoa.Id}.");

            var horario = await _horarioRepository.ObterPorIdAsync(horarioPessoa.HorarioId, cancellationToken);
            if (horario?.Jornadas is null)
                throw new InvalidOperationException($"Horário ou jornadas não encontrados para o ID: {horarioPessoa.HorarioId}.");

            var marcacoesPorData = marcacoes
               .GroupBy(m => DateOnly.FromDateTime(m.DataHora.Date))
               .ToDictionary(g => g.Key, g => g.ToArray());

            const int tamanhoLote = 100;

            foreach (var lote in diaApontamentos.Chunk(tamanhoLote))
            {
                foreach (var diaApontamento in lote)
                {
                    var dataAtual = diaApontamento.DataApontamento;
                    var jornadaPrevista = 0;
                    if (diaApontamento.HorarioId is null)
                    {
                         jornadaPrevista = horario.Jornadas
                        .FirstOrDefault(j => j.DiaSemana == dataAtual.DayOfWeek)?.MinutosCargaHoraria ?? 0;
                    }
                    else
                    {
                        var horarioEscala = await _horarioRepository.ObterPorIdAsync((int)diaApontamento.HorarioId, cancellationToken);
                        jornadaPrevista = horarioEscala.Jornadas
                        .FirstOrDefault(j => j.DiaSemana == dataAtual.DayOfWeek)?.MinutosCargaHoraria ?? 0;
                    }
                    int totalTrabalhado = 0;

                    if (marcacoesPorData.TryGetValue(dataAtual, out var marcacoesDoDia))
                    {
                        for (int i = 0; i + 1 < marcacoesDoDia.Length; i += 2)
                        {
                            var entrada = marcacoesDoDia[i].DataHora;
                            var saida = marcacoesDoDia[i + 1].DataHora;
                            totalTrabalhado += (int)(saida - entrada).TotalMinutes;
                        }
                    }
                    
                    diaApontamento.MinutosTrabalhados = totalTrabalhado;
                    diaApontamento.MinutosFalta = 0;
                    diaApontamento.MinutosAtraso = 0;
                    diaApontamento.MinutosExtra = 0;

                    if (jornadaPrevista > 0)
                    {
                        if (totalTrabalhado == 0)
                        {
                            diaApontamento.MinutosFalta = jornadaPrevista;
                        }
                        else if (totalTrabalhado < jornadaPrevista)
                        {
                            diaApontamento.MinutosAtraso = jornadaPrevista - totalTrabalhado;
                        }
                        else if (totalTrabalhado > jornadaPrevista)
                        {
                            diaApontamento.MinutosExtra = totalTrabalhado - jornadaPrevista;
                        }
                    }
                    else if (totalTrabalhado > 0)
                    {
                        diaApontamento.MinutosExtra = totalTrabalhado;
                    }
                }
                
                await _DiaApontamentoRepository.AtualizarDiasAsync(lote, cancellationToken);
            }
        }
    }
}
