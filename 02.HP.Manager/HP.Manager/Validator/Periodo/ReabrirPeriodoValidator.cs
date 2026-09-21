using FluentValidation;
using HP.Manager.DTOs.Periodo;
using HP.Manager.Implementation;
using HP.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Validator.Periodo
{
    public class ReabrirPeriodoValidator : AbstractValidator<ReabriPeridoDto>
    {
        private readonly IPeriodoManager _periodoManager;
        public ReabrirPeriodoValidator(IPeriodoManager periodoManager)
        {
            _periodoManager = periodoManager;
            RuleFor(x => x)
              .MustAsync(PeriodoAbertoAsync)
              .WithMessage("O periodo já está aberto");

        }
        private async Task<bool> PeriodoAbertoAsync(ReabriPeridoDto dto, CancellationToken cancellationToken)
        {
            var periodo = await _periodoManager.ObterPorIdAsync(dto.Id, cancellationToken);

            return periodo != null && !periodo.Aberto;
        }
    }
}
