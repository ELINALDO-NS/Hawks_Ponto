using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Periodo;
using HP.Manager.Interfaces;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class PeriodoManager(IPeriodoRepository _repository, IMapper _mapper) : IPeriodoManager
    {
        public async Task<PeriodoDto> AdicionarAsync(PeriodoDto periododto, CancellationToken cancellationToken)
        {
            var periodo = _mapper.Map<Periodo>(periododto);
            var periodoinserido = await _repository.AdicionarAsync(periodo, cancellationToken);
            return _mapper.Map<PeriodoDto>(periodoinserido);
        }
        public async Task<PeriodoDto> AtualizarAsync(PeriodoDto periododto, CancellationToken cancellationToken)
        {
            var periodo = _mapper.Map<Periodo>(periododto);
            await _repository.AtualizarAsync(periodo, cancellationToken);
            return _mapper.Map<PeriodoDto>(periodo);
        }
        public async Task<PeriodoDto?> FecharAsync(FechaPeriodoDto fechaPeriodoDto, CancellationToken cancellationToken)
        {
            var periodo = await _repository.ObterPorIdAsync(fechaPeriodoDto.Id, cancellationToken);
            if (periodo is null)
                throw new InvalidOperationException("Periodo não encontrado");

            if (!periodo.Aberto)
                throw new InvalidOperationException("O periodo já está fechado");

            if (fechaPeriodoDto.DataFim < periodo.DataInicio)
                throw new InvalidOperationException("Data de fim não pode ser anterior à data de início do período.");

            if (fechaPeriodoDto.DataFim > DateOnly.FromDateTime(DateTime.Now))
                throw new InvalidOperationException("A data de fim do periodo deve ser anterior a data atual");

            if (periodo.DataFim == fechaPeriodoDto.DataFim)
            {
                periodo.Aberto = false;
                periodo.DataUltAtualizacao = DateTimeOffset.Now;
                await _repository.AtualizarAsync(periodo, cancellationToken);
                return _mapper.Map<PeriodoDto>(periodo);
            }

            if (periodo.DataFim == new DateOnly(2099, 12, 31))
            {
                periodo.DataUltAtualizacao = DateTimeOffset.Now;
                periodo.DataFim = fechaPeriodoDto.DataFim;
                periodo.Aberto = false;
                var periodoNovo = new Periodo()
                {
                    Aberto = true,
                    DataInicio = fechaPeriodoDto.DataFim.AddDays(1),
                    DataFim = new DateOnly(2099, 12, 31),
                    DataUltAtualizacao = DateTimeOffset.Now,
                    EmpresaId = 1
                };

                await _repository.AdicionarAsync(periodoNovo, cancellationToken);
                await _repository.FecharAsync(periodo, cancellationToken);
                return _mapper.Map<PeriodoDto>(periodo);

            }
            return null;

        }
        public async Task<PeriodoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var periodo = await _repository.ObterPorIdAsync(id, cancellationToken);
            return _mapper.Map<PeriodoDto>(periodo);
        }
        public async Task<IEnumerable<PeriodoDto>?> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var periodos = await _repository.ObterTodosAsync(cancellationToken);
            return _mapper.Map<List<PeriodoDto>>(periodos);
        }
        public async Task<PeriodoDto?> ReabrirAsync(ReabriPeridoDto periododto, CancellationToken cancellationToken)
        {
            var periodo = await _repository.ObterPorIdAsync(periododto.Id, cancellationToken);

            if (periodo is not null && periodo.DataFim != new DateOnly(2099, 12, 31))
            {
                periodo.Aberto = true;
                periodo.DataUltAtualizacao = DateTimeOffset.Now;
                await _repository.ReabrirAsync(periodo, cancellationToken);
            }
            return _mapper.Map<PeriodoDto?>(periodo);
        }
        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var periodo = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (periodo is null)
            {
                throw new InvalidOperationException("Periodo Não encontrado");
            }
            if (periodo.DataFim == new DateOnly(2099, 12, 31))
            {
                throw new InvalidOperationException("O periodo atual não pode ser removido");
            }
            if (periodo.Aberto)
            {
                throw new InvalidOperationException("Periodos fechados não podem ser removido");
            }
            var periodos = await _repository.ObterTodosAsync(cancellationToken);
            var existeMaisRecente = periodos
                .Where(p => p.Id != periodo.Id)
                .Where(p => p.DataFim != new DateOnly(2099, 12, 31))
                .Any(p => p.DataInicio > periodo.DataInicio);

            if (existeMaisRecente)
            {
                throw new InvalidOperationException(
                    "Só é possível remover períodos em ordem decrescente. Remova primeiro o período mais recente.");
            }
            var removido = await _repository.RemoverAsync(periodo, cancellationToken);
            if (removido)
            {
                var periodoAtual = periodos.FirstOrDefault(p => p.DataFim == new DateOnly(2099, 12, 31));
                if (periodoAtual is not null)
                {
                    periodoAtual.DataInicio = periodo.DataInicio;
                    await _repository.AtualizarAsync(periodoAtual, cancellationToken);
                }
            }

            return removido;
        }

    }
}
