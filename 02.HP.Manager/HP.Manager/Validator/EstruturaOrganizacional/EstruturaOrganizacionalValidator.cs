using FluentValidation;
using HP.Manager.DTOs.EstruturaOrganizacional;


namespace HP.Manager.Validator.EstruturaOrganizacional
{
    public class EstruturaOrganizacionalValidator : AbstractValidator<EstruturaOrganizacionalDto>
    {
        public EstruturaOrganizacionalValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.EmpresaId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
