using FluentValidation;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Validator.Endereco;

namespace HP.Manager.Validator.Empresa
{
    public class NovaEmpresaValidator : AbstractValidator<AdicionaEmpresaDto>
    {
        public NovaEmpresaValidator()
        {
            RuleFor(x => x.CnpjCpf).NotEmpty().NotNull().Must(cnpj => ValidaCNPJ.Validar(cnpj)).WithMessage("CNPJ informado é inválido.");
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x=> x.RazaoSocial).NotEmpty().NotNull().Length(3, 150).WithMessage("A Razão Social deve ter entre 3 e 150 caracteres.").Must(razao => TextoSemEspacosEmBranco(razao));
            RuleFor(x=>x.Email).EmailAddress().MaximumLength(150).WithMessage("O e-mail deve no maximo 150 caracteres.");
            RuleFor(x => x.Endrereco).SetValidator(new NovoEnderecoValidator());
        }

        private bool TextoSemEspacosEmBranco(string? texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }
    }
}
