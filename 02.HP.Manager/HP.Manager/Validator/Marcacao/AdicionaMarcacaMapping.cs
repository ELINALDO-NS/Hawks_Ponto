using FluentValidation;
using HP.Core.Enums;
using HP.Manager.DTOs.Marcacao;
using HP.Manager.Validator.Pessoa;


namespace HP.Manager.Validator.Marcacao
{
    public class AdicionaMarcacaMapping : AbstractValidator<AdicionaMarcacaoDto>
    {
        public AdicionaMarcacaMapping()
        {
            RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Must(cpf => ValidaPIS_CPF.ValidaCpf(cpf)).WithMessage("CPF inválido.");

            RuleFor(x => x.DataHora)
              .NotEqual(default(DateTimeOffset)).WithMessage("Data/hora é obrigatória.");

            RuleFor(x => x.TipoMarcacao)
                .IsInEnum().WithMessage("Tipo de marcação inválido.");

            RuleFor(x => x.OrigemMarcacao)
                .IsInEnum().WithMessage("Origem da marcação inválida.");

            RuleFor(x => x.RelogioId)
                .GreaterThan(0)
                .When(x => x.RelogioId.HasValue)
                .WithMessage("Id do relógio inválido.");

            RuleFor(x => x.NSR)
                .GreaterThan(0)
                .When(x => x.NSR.HasValue)
                .WithMessage("NSR inválido.");

            
            RuleFor(x => x.Justificativa)
                .NotEmpty()
                .When(x => x.OrigemMarcacao == OrigemMarcacao.Manual)
                .WithMessage("Justificativa é obrigatória para marcação manual.");

            
            RuleFor(x => x.RelogioId)
                .NotNull()
                .When(x => x.OrigemMarcacao == OrigemMarcacao.REPC)
                .WithMessage("Id do relógio é obrigatório para marcação REP-C.");
        }
    }
}
