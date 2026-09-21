using FluentValidation;
using HP.Manager.DTOs.Periodo;
using HP.Manager.Interfaces;


namespace HP.Manager.Validator.Periodo
{
    public class FechaPeriodoValidator : AbstractValidator<FechaPeriodoDto>
    {
        private readonly IPeriodoManager _periodoManager;
        public FechaPeriodoValidator(IPeriodoManager periodoManager)
        {
            _periodoManager = periodoManager;
            RuleFor(x => x.Id)
             .MustAsync(PeriodoExisteAsync)
             .WithMessage("Periodo não encontrado");

            RuleFor(x => x)
             .MustAsync(DataFimCehamentoAsync)
             .WithMessage("A data de fim do periodo deve ser anterior a data atual");

            RuleFor(x => x)
                .MustAsync(PeriodoFechadoAsync)
                .WithMessage("O periodo já está fechado")
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .MustAsync(DataFimNaoAnteriorAsync)
                        .WithMessage("Data de fim não pode ser anterior à data de início do período.");
                });
        }

        private async Task<bool> PeriodoExisteAsync(int id, CancellationToken cancellationToken)
        {
            var periodo = await _periodoManager.ObterPorIdAsync(id, cancellationToken);
            return periodo is not null;
        }

        private async Task<bool> PeriodoFechadoAsync(FechaPeriodoDto dto, CancellationToken cancellationToken)
        {
            var periodo = await _periodoManager.ObterPorIdAsync(dto.Id, cancellationToken);
            return periodo != null && periodo.Aberto;
        }

        private async Task<bool> DataFimNaoAnteriorAsync(FechaPeriodoDto dto, CancellationToken cancellationToken)
        {
            var periodo = await _periodoManager.ObterPorIdAsync(dto.Id, cancellationToken);
            return periodo is not null && dto.DataFim >= periodo.DataInicio;
        }
        private async Task<bool> DataFimCehamentoAsync(FechaPeriodoDto dto, CancellationToken cancellationToken)
        {
            var periodo = await _periodoManager.ObterPorIdAsync(dto.Id, cancellationToken);
            if (periodo is not null && dto.DataFim > DateOnly.FromDateTime(DateTime.Now))
            {
                return false;
            }
            return true;
        }

    }
}
