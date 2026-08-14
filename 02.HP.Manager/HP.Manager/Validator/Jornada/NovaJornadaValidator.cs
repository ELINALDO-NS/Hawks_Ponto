using FluentValidation;
using HP.Manager.DTOs.Jornada;


namespace HP.Manager.Validator.Jornada
{
    public class NovaJornadaValidator : AbstractValidator<JornadaDto>
    {
        public NovaJornadaValidator()
        {
            RuleFor(x => x.DiaSemana)
                .IsInEnum().WithMessage("O dia da semana informado é inválido.");
       
            RuleFor(x => x.Entrada1)
                 .NotEmpty().WithMessage("A Entrada 1 é obrigatória.");

            RuleFor(x => x.Saida1)
                .NotEmpty().WithMessage("A Saída 1 é obrigatória.")
                .GreaterThan(x => x.Entrada1)
                .WithMessage("A Saída 1 deve ser posterior à Entrada 1.");

            
            When(x => x.Entrada2.HasValue || x.Saida2.HasValue, () =>
            {
                RuleFor(x => x.Entrada2)
                    .NotNull().WithMessage("A Entrada 2 é obrigatória quando a Saída 2 for informada.");

                RuleFor(x => x.Saida2)
                    .NotNull().WithMessage("A Saída 2 é obrigatória quando a Entrada 2 for informada.");

                
                RuleFor(x => x.Entrada2)
                    .GreaterThan(x => x.Saida1)
                    .When(x => x.Entrada2.HasValue && x.Saida1 != default)
                    .WithMessage("A Entrada 2 deve ser posterior à Saída 1.");

                
                RuleFor(x => x.Saida2)
                    .GreaterThan(x => x.Entrada2)
                    .When(x => x.Saida2.HasValue && x.Entrada2.HasValue)
                    .WithMessage("A Saída 2 deve ser posterior à Entrada 2.");
            });

            
            When(x => x.Entrada3.HasValue || x.Saida3.HasValue, () =>
            {
                
                RuleFor(x => x)
                    .Must(x => x.Entrada2.HasValue && x.Saida2.HasValue)
                    .WithMessage("Não é possível informar o 3º turno sem preencher o 2º turno completamente.");

                RuleFor(x => x.Entrada3)
                    .NotNull().WithMessage("A Entrada 3 é obrigatória quando a Saída 3 for informada.");

                RuleFor(x => x.Saida3)
                    .NotNull().WithMessage("A Saída 3 é obrigatória quando a Entrada 3 for informada.");

                
                RuleFor(x => x.Entrada3)
                    .GreaterThan(x => x.Saida2)
                    .When(x => x.Entrada3.HasValue && x.Saida2.HasValue)
                    .WithMessage("A Entrada 3 deve ser posterior à Saída 2.");

                
                RuleFor(x => x.Saida3)
                    .GreaterThan(x => x.Entrada3)
                    .When(x => x.Saida3.HasValue && x.Entrada3.HasValue)
                    .WithMessage("A Saída 3 deve ser posterior à Entrada 3.");
            });
        }
    }
}
