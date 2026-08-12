using FluentValidation;
using HP.Manager.DTOs.Cargo;

namespace HP.Manager.Validator.Cargo
{
    public class NovoCargoValidator : AbstractValidator<AdicionaCargoDto>
    {
        public NovoCargoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.EmpresaId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
