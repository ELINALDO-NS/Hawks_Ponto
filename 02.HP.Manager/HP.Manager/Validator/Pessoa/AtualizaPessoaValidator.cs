using FluentValidation;
using HP.Manager.DTOs.Pessoa;
using HP.Manager.Validator.Endereco;

namespace HP.Manager.Validator.Pessoa
{
    public class AtualizaPessoaValidator : AbstractValidator<AtualizaPessoaDto>
    {
        public AtualizaPessoaValidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("Id deve ser maior que zero.");

            RuleFor(x => x.EstruturaOrganizacional.Id)
              .GreaterThan(0).WithMessage("EstruturaId deve ser maior que zero.")
              .NotNull().WithMessage("EstruturaId não pode ser nulo.")
              .When(x => x.EstruturaOrganizacional is not null);

            RuleFor(x => x.Horario.Id)
              .GreaterThan(0).WithMessage("Horario.Id deve ser maior que zero.")
              .NotNull().WithMessage("Horario.Id não pode ser nulo.")
              .When(x => x.Horario is not null);

            RuleFor(x => x.Cargo.Id)
               .GreaterThan(0).WithMessage("CargoId deve ser maior que zero.")
               .When(x => x.Cargo is not null);

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("EmpresaId deve ser maior que zero.");

            RuleFor(x => x.Matricula)
                .GreaterThan(0).WithMessage("Matrícula deve ser maior que zero.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.DataNascimento)
                .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser anterior à data atual.")
                .When(x => x.DataNascimento.HasValue);

            RuleFor(x => x.DataAdmissao)
                .NotEmpty().WithMessage("Data de admissão é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now.AddDays(5)).WithMessage("Data de admissão não pode maior 5 dias somados a data atual.")
                .GreaterThan(x => x.DataNascimento!.Value)
                .When(x => x.DataNascimento.HasValue)
                .WithMessage("Data de admissão deve ser posterior à data de nascimento.");

            RuleFor(x => x.DataDemissao)
                .GreaterThanOrEqualTo(x => x.DataAdmissao)
                .When(x => x.DataDemissao.HasValue)
                .WithMessage("Data de demissão deve ser posterior à data de admissão.");


            RuleFor(x => x.Rg)
                .MaximumLength(20).WithMessage("RG deve ter no máximo 20 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Rg));

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .Must(cpf => ValidaPIS_CPF.ValidaCpf(cpf)).WithMessage("CPF inválido.");

            RuleFor(x => x.Pis)
                .NotEmpty().WithMessage("PIS é obrigatório.")
                .Must((string pis) => ValidaPIS_CPF.ValidaPis(pis))
                .WithMessage("PIS inválido.");

            RuleFor(x => x.Telefone)
                .Matches(@"^\(\d{2}\)\s?\d{4}-?\d{4}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Telefone))
                .WithMessage("Telefone inválido. Formato esperado: (99) 9999-9999.");

            RuleFor(x => x.TelefoneCelular)
                .Matches(@"^\(\d{2}\)\s?9\d{4}-?\d{4}$")
                .When(x => !string.IsNullOrWhiteSpace(x.TelefoneCelular))
                .WithMessage("Telefone celular inválido. Formato esperado: (99) 99999-9999.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("E-mail inválido.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.DataControlaPonto)
                .NotEmpty()
                .When(x => x.ControlaPonto)
                .WithMessage("Data de início do controle de ponto é obrigatória quando ControlaPonto for verdadeiro.");

            RuleFor(x => x.DataNaoControlaPonto)
                .GreaterThanOrEqualTo(x => x.DataControlaPonto!.Value)
                .When(x => x.DataNaoControlaPonto.HasValue && x.DataControlaPonto.HasValue)
                .WithMessage("Data de encerramento do controle de ponto deve ser posterior à data de início.");

            RuleFor(x => x.Sexo)
             .IsInEnum()
             .WithMessage("Sexo deve ser um valor válido: 1 (Masculino) , 2 (Feminino) 3 (Outro).");

            RuleFor(x => x.BaseHoras)
                .GreaterThan(0).WithMessage("Base de horas deve ser maior que zero.");


            RuleFor(x => x.Endereco)
                .SetValidator(new NovoEnderecoValidator()!)
                .When(x => x.Endereco != null);
        }

    }

}
