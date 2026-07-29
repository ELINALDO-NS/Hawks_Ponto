using FluentValidation;
using HP.Manager.DTOs.Endereco;


namespace HP.Manager.Validator.Endereco
{
    public class NovoEnderecoValidator : AbstractValidator<AdicionaEnderecoDto>
    {
        public NovoEnderecoValidator()
        {

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{5}-?\d{3}$").WithMessage("O CEP deve estar em um formato válido (ex: 12345-678 ou 12345678).");


            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("O Logradouro (Rua/Avenida) é obrigatório.")
                .Length(3, 150).WithMessage("O Logradouro deve ter entre 3 e 150 caracteres.");


            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("O Número é obrigatório (use 'S/N' se não houver).")
                .MaximumLength(20).WithMessage("O Número deve ter no máximo 20 caracteres.");


            RuleFor(x => x.Complemento)
                .MaximumLength(100).WithMessage("O Complemento não pode exceder 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Complemento));


            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O Bairro é obrigatório.")
                .Length(2, 80).WithMessage("O Bairro deve ter entre 2 e 80 caracteres.");


            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A Cidade é obrigatória.")
                .Length(2, 100).WithMessage("A Cidade deve ter entre 2 e 100 caracteres.");


            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("A UF é obrigatória.")
                .Length(2).WithMessage("A UF deve conter exatamente 2 letras (ex: BA, CE).")
                .Must(ValidaUf).WithMessage("A UF informada é inválida.");
        }


        private bool ValidaUf(string uf)
        {
            var ufsValidas = new HashSet<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

            return ufsValidas.Contains(uf?.ToUpperInvariant() ?? string.Empty);
        }
    }
}

