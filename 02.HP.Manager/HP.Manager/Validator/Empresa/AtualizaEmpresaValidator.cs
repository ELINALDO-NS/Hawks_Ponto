using FluentValidation;
using HP.Manager.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Validator.Empresa
{
    public class AtualizaEmpresaValidator : AbstractValidator<AtualizaEmpresaDto>
    {
        public AtualizaEmpresaValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.CnpjCpf).NotEmpty().NotNull().Must(cnpj => ValidaCNPJ.Validar(cnpj)).WithMessage("CNPJ informado é inválido.");
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.RazaoSocial).NotEmpty().NotNull().Length(3, 150).WithMessage("A Razão Social deve ter entre 3 e 150 caracteres.").Must(razao => TextoSemEspacosEmBranco(razao));
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.IdEndrereco).NotEmpty().NotNull().GreaterThan(0);

        }

        private bool TextoSemEspacosEmBranco(string? texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }
    }
}
