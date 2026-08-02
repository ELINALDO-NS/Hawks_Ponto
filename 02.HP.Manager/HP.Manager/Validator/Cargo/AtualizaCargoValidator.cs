using FluentValidation;
using HP.Manager.DTOs.Cargo;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Validator.Cargo
{
    public class AtualizaCargoValidator:AbstractValidator<AtualizaCargoDto>
    {
        public AtualizaCargoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Codigo).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Descricao).NotEmpty().NotNull();
            RuleFor(x => x.EmpresaId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
