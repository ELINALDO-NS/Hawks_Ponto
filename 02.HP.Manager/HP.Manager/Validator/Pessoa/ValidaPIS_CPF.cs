using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Validator.Pessoa
{
    public static class ValidaPIS_CPF
    {
        public static bool ValidaPis(string pis)
        {
            if (string.IsNullOrWhiteSpace(pis))
                return false;

            pis = new string(pis.Where(char.IsDigit).ToArray());

            if (pis.Length != 11)
                return false;

            if (pis.Distinct().Count() == 1)
                return false;

            var multiplicadores = new[] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (pis[i] - '0') * multiplicadores[i];

            var resto = soma % 11;
            var digitoVerificador = resto < 2 ? 0 : 11 - resto;

            return (pis[10] - '0') == digitoVerificador;
        }
        public static bool ValidaCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            var soma = tempCpf.Select((c, i) => (c - '0') * multiplicador1[i]).Sum();
            var resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();
            tempCpf += digito;

            soma = tempCpf.Select((c, i) => (c - '0') * multiplicador2[i]).Sum();
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto;

            return cpf.EndsWith(digito);
        }

    }
}
