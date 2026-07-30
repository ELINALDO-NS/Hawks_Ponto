using HP.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class Endereco
    {
        public Endereco() { }
        public int Id { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Logradouro
        {
            get; set => field = value.ToTitleCase() ?? string.Empty;
        } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string Bairro
        {
            get; set => field = value.ToTitleCase() ?? string.Empty;
        } = string.Empty;
        public required string Cidade
        {
            get; set => field = value.ToTitleCase() ?? string.Empty;
        } = string.Empty;
        public string Uf
        {
            get;
            set => field = value?.ToUpperSafe() ?? string.Empty;
        } = string.Empty;
    }
}
