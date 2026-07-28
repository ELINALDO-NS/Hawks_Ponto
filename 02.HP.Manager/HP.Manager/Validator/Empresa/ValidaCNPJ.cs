
using System.Text.RegularExpressions;

namespace HP.Manager.Validator.Empresa
{
    public static class ValidaCNPJ
    {
        private static readonly int[] Peso1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Peso2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public static bool Validar(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = Regex.Replace(cnpj.Trim().ToUpperInvariant(), @"[^0-9A-Z]", "");

            if (cnpj.Length != 14)
                return false;

            // Bloqueia sequências com todos os caracteres iguais
            if (cnpj.Distinct().Count() == 1)
                return false;

            // Os 2 últimos dígitos verificadores devem ser sempre numéricos
            if (!char.IsDigit(cnpj[12]) || !char.IsDigit(cnpj[13]))
                return false;

            var digito1 = CalcularDigito(cnpj.Substring(0, 12), Peso1);
            var digito2 = CalcularDigito(cnpj.Substring(0, 12) + digito1, Peso2);

            return cnpj.EndsWith($"{digito1}{digito2}");
        }
        private static int CalcularDigito(string baseCnpj, int[] pesos)
        {
            var soma = 0;
            for (var i = 0; i < baseCnpj.Length; i++)
            {
                // Valor ASCII do caractere menos 48 (funciona para '0'-'9' e 'A'-'Z')
                var valor = baseCnpj[i] - '0';
                soma += valor * pesos[i];
            }

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}

