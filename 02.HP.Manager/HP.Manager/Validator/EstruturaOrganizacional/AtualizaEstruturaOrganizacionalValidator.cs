using FluentValidation;
using HP.Manager.DTOs.EstruturaOrganizacional;


namespace HP.Manager.Validator.EstruturaOrganizacional
{
    public class AtualizaEstruturaOrganizacionalValidator : AbstractValidator<AtualizaEstruturaOrganizacionalDto>
    {
        public AtualizaEstruturaOrganizacionalValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.EmpresaId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
