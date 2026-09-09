using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.Interfaces;


namespace HP.Manager.Implementation
{
    public class CalculaAtrasosExtraFaltasManager(IMarcacaoRepository _MarcacaoRepository, IDiaApontamentoRepository _DiaApontamentoRepository, IHorarioRepository _horarioRepository) : ICalculaAtrasosExtraFaltasManager
    {
        public async Task CalculaAdicionalNoturno(IReadOnlyCollection<DiaApontamento> diaApontamentos, List<Marcacao> marcacoes, Horario horario, Pessoa pessoa, CancellationToken cancellationToken)
        {
            var marcacoesPorData = marcacoes
                 .GroupBy(m => ObterDataTurno(m.DataHora, horario.InicioFimDia.ToTimeSpan()))
                 .ToDictionary(g => g.Key, g => g.OrderBy(m => m.DataHora).ToArray());


            foreach (var diaApontamento in diaApontamentos)
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
                    marcacoesPorData = marcacoes
                         .GroupBy(m => ObterDataTurno(m.DataHora, horarioEscala.InicioFimDia.ToTimeSpan()))
                         .ToDictionary(g => g.Key, g => g.OrderBy(m => m.DataHora).ToArray());
                }

                int totalNoturno = 0;

                if (marcacoesPorData.TryGetValue(dataAtual, out var marcacoesDoDia))
                {
                    for (int i = 0; i + 1 < marcacoesDoDia.Length; i += 2)
                    {
                        var entrada = marcacoesDoDia[i].DataHora;
                        var saida = marcacoesDoDia[i + 1].DataHora;
                        totalNoturno += CalcularMinutosNoturnos(entrada, saida);
                    }
                }

                diaApontamento.MinutosAdicionalNoturno = (int)Math.Ceiling(totalNoturno * 1.14286);
            }
        }
        DateOnly ObterDataTurno(DateTimeOffset dataHora, TimeSpan InicioFimDia)
        {
            var hora = dataHora.TimeOfDay;

            if (hora < InicioFimDia)
                return DateOnly.FromDateTime(dataHora.DateTime.AddDays(-1));

            return DateOnly.FromDateTime(dataHora.DateTime);
        }
        int CalcularMinutosNoturnos(DateTimeOffset entrada, DateTimeOffset saida)
        {
            int totalNoturno = 0;
            var diaReferencia = entrada.Date;

            while (diaReferencia <= saida.Date)
            {

                var inicioNoturno = new DateTimeOffset(diaReferencia, TimeSpan.FromHours(-3)).AddHours(22);
                var fimNoturno = inicioNoturno.AddHours(7);


                var inicioIntersecao = entrada > inicioNoturno ? entrada : inicioNoturno;
                var fimIntersecao = saida < fimNoturno ? saida : fimNoturno;

                if (fimIntersecao > inicioIntersecao)
                {
                    totalNoturno += (int)(fimIntersecao - inicioIntersecao).TotalMinutes;
                }

                diaReferencia = diaReferencia.AddDays(1);
            }

            return totalNoturno;

        }
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


            DateOnly ObterDataTurno(DateTimeOffset dataHora, TimeSpan InicioFimDia)
            {
                var hora = dataHora.TimeOfDay;

                if (hora < InicioFimDia)
                    return DateOnly.FromDateTime(dataHora.DateTime.AddDays(-1));

                return DateOnly.FromDateTime(dataHora.DateTime);
            }
            var marcacoesPorData = marcacoes
                 .GroupBy(m => ObterDataTurno(m.DataHora, horario.InicioFimDia.ToTimeSpan()))
                 .ToDictionary(g => g.Key, g => g.OrderBy(m => m.DataHora).ToArray());

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
                        marcacoesPorData = marcacoes
                             .GroupBy(m => ObterDataTurno(m.DataHora, horarioEscala.InicioFimDia.ToTimeSpan()))
                             .ToDictionary(g => g.Key, g => g.OrderBy(m => m.DataHora).ToArray());
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
                    await CalculaAdicionalNoturno(diaApontamentos, marcacoes, horario, pessoa, cancellationToken);
                }

                await _DiaApontamentoRepository.AtualizarDiasAsync(lote, cancellationToken);
            }
        }

    }
}
