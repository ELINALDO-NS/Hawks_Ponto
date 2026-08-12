using FluentValidation;
using HP.Manager.DTOs.Cargo;


namespace HP.Manager.Validator.Cargo
{
    public class CargoValidator : AbstractValidator<CargoDto>
    {
        public CargoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.EmpresaId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
