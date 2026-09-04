using FluentValidation;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Pessoa;
using HP.Manager.Interfaces;
using HP.Manager.Validator.Endereco;
using System.Security.Cryptography.X509Certificates;

namespace HP.Manager.Validator.Pessoa
{
    public class NovaPessoaValidator : AbstractValidator<AdicionaPessoaDto>
    {
        private readonly IEstruturaOrganizacionalManager _Estrutura;
        private readonly ICargoManager _Cargo;
        private readonly IHorarioManager _Horario;
        private readonly IPessoaManager _Pessoa;
        private readonly IEmpresaManager _Empresa;
        public NovaPessoaValidator(IEmpresaManager empresa, IPessoaManager pessoa, IEstruturaOrganizacionalManager estrutura, ICargoManager cargo, IHorarioManager horario)
        {
            _Estrutura = estrutura;
            _Cargo = cargo;
            _Horario = horario;
            _Pessoa = pessoa;
            _Empresa = empresa;

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("EmpresaId deve ser maior que zero.");

            RuleFor(x => x.EstruturaOrganizacional.Id)
               .GreaterThan(0).WithMessage("EstruturaId deve ser maior que zero.")
               .NotNull().WithMessage("EstruturaId não pode ser nulo.")
               .When(x => x.EstruturaOrganizacional is not null);

            RuleFor(x => x.EstruturaOrganizacional)
               .NotNull().WithMessage("EstruturaOrganizacional não pode ser nulo.");


            RuleFor(x => x.EstruturaOrganizacional)
                .MustAsync(async (estrutura, cancellationToken) =>
                    await ValidaEstruturaOrganizacional(estrutura.Id, cancellationToken))
                .WithMessage("EstruturaOrganizacional não cadastrada.")
                .When(x => x.EstruturaOrganizacional is not null);

            RuleFor(x => x.Cargo)
                .MustAsync(async (cargo, cancellationToken) =>
                    await ValidaEstruturaOrganizacional(cargo.Id, cancellationToken))
                .WithMessage("Cargo não cadastrado.")
                .When(x => x.Cargo is not null);

            RuleFor(x => x.Horario)
                 .MustAsync(async (horario, cancellationToken) =>
                     await ValidaHorario(horario.Id, cancellationToken))
                 .WithMessage("Horario não cadastrado.")
                 .When(x => x.Horario is not null);

            RuleFor(x => x.EmpresaId)
                 .MustAsync(async (empresa, cancellationToken) =>
                     await ValidaEmpresa(empresa, cancellationToken))
                 .WithMessage("Empresa não encontrada.");
                 

            RuleFor(x => x.Horario)
                .NotNull().WithMessage("Horario não pode ser nulo.");

            RuleFor(x => x.Horario.Id)
               .GreaterThan(0).WithMessage("Horario.Id deve ser maior que zero.")
               .NotNull().WithMessage("Horario.Id não pode ser nulo.")
               .When(x => x.Horario is not null);

            RuleFor(x => x.Cargo.Id)
               .GreaterThan(0).WithMessage("Cargo.Id deve ser maior que zero.")
               .When(x => x.Cargo is not null);

            RuleFor(x => x.Matricula)
                .GreaterThan(0).WithMessage("Matrícula deve ser maior que zero.");

            RuleFor(x => x.Matricula)
                 .MustAsync(async (mat, cancellationToken) =>
                     await ValidaMatricula(mat, cancellationToken))
                 .WithMessage("A matrícula '{PropertyValue}' já está em uso no sistema.");


            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.DataNascimento)
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Data de nascimento deve ser anterior à data atual.")
                .When(x => x.DataNascimento.HasValue);

            RuleFor(x => x.DataAdmissao)
                  .NotEmpty().WithMessage("Data de admissão é obrigatória.")
                  .LessThanOrEqualTo(DateTime.Today.AddDays(5)).WithMessage("Data de admissão não pode maior 5 dias somados a data atual.")
                  .GreaterThan(x => x.DataNascimento!.Value.ToDateTime(TimeOnly.MinValue))
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
                .Must(pis => ValidaPIS_CPF.ValidaPis(pis)).WithMessage("PIS inválido.");

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
        public async Task<bool> ValidaEmpresa(int Id, CancellationToken cancellation)
        {
            var empresa = await _Empresa.ObterPorIdAsync(Id, cancellation);
            return empresa is not null;
        }
        public async Task<bool> ValidaEstruturaOrganizacional(int Id, CancellationToken cancellation)
        {
            var estrutura = await _Estrutura.ObterPorIdAsync(Id, cancellation);
            return estrutura is not null;
        }

        public async Task<bool> ValidaCargo(int Id, CancellationToken cancellation)
        {
            var cargo = await _Cargo.ObterPorIdAsync(Id, cancellation);
            return cargo is not null;
        }

        public async Task<bool> ValidaHorario(int Id, CancellationToken cancellation)
        {
            var horario = await _Horario.ObterPorIdAsync(Id, cancellation);
            return horario is not null;
        }

        public async Task<bool> ValidaMatricula(int Matricula, CancellationToken cancellation)
        {
            var matricula = await _Pessoa.ObterPorMatriculaAsync(Matricula, cancellation);
            if (matricula is not null)
            {
                return false;
            }
            return true;
        }
    }
}
