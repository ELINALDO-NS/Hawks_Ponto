using FluentValidation;
using HP.Manager.DTOs.Horario;
using HP.Manager.Validator.Jornada;


namespace HP.Manager.Validator.Horario
{
    public class NovoHorarioValidator:AbstractValidator<AdicionaHorarioDto>
    {
        public NovoHorarioValidator()
        {
            RuleFor(x => x.EmpresaId)
          .GreaterThan(0)
          .WithMessage("EmpresaId deve ser informado e maior que zero.");

            RuleFor(x => x.Codigo)
                .NotEmpty()
                    .WithMessage("Código é obrigatório.")
                .MaximumLength(20)
                    .WithMessage("Código deve ter no máximo 20 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                    .WithMessage("Descrição é obrigatória.")
                .MaximumLength(100)
                    .WithMessage("Descrição deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Jornadas)
                .NotNull()
                    .WithMessage("É necessário informar ao menos uma jornada.")
                .Must(j => j != null && j.Count > 0)
                    .WithMessage("A lista de jornadas não pode estar vazia.");

            RuleForEach(x => x.Jornadas)
                .SetValidator(new NovaJornadaValidator())
                .When(x => x.Jornadas != null && x.Jornadas.Count > 0);
        }
    }
}
