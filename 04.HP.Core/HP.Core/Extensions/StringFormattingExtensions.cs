using System.Globalization;
using System.Text.RegularExpressions;


namespace HP.Core.Extentions
{
    public static class StringFormattingExtensions
    {
        private static readonly Regex ApenasNumerosRegex = new Regex(@"[^\d]", RegexOptions.Compiled);

        public static string ToTitleCase(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        public static string ToUpperSafe(this string? input)
        {
            return input?.ToUpper() ?? string.Empty;
        }

        public static string RemoveFormatacao(this string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return string.Empty;

            return ApenasNumerosRegex.Replace(documento, "");
        }

        private static string FormatarCPF(this string cpf)
        {
            var numeros = cpf.RemoveFormatacao();

            if (numeros.Length != 11)
                return cpf; 

            return Convert.ToUInt64(numeros).ToString(@"000\.000\.000\-00");
        }

        private static string FormatarCNPJ(this string cnpj)
        {
            var numeros = cnpj.RemoveFormatacao();

            if (numeros.Length != 14)
                return cnpj; 

            return Convert.ToUInt64(numeros).ToString(@"00\.000\.000\/0000\-00");
        }
      
        public static string FormatarCPF_CNPJ(this string documento)
        {
            var numeros = documento.RemoveFormatacao();

            return numeros.Length switch
            {
                11 => numeros.FormatarCPF(),
                14 => numeros.FormatarCNPJ(),
                _ => documento
            };
        }
    }
}
