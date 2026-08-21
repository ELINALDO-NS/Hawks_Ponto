using HP.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class Empresa
    {
        public Empresa() { }
        public int Id { get; set; }
        
        public int Codigo { get; set; }
        public string RazaoSocial
        {
            get;
            set => field = value.ToTitleCase() ?? string.Empty;

        } = null!;
        public string CnpjCpf { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string? Site { get; set; }
        public string? Email { get; set; }
        public string TipoEmpresa { get; set; } = null!;
        public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now.ToLocalTime();
        public  DateTimeOffset? DataUltAtualizacao { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = null!;
        public bool Portaria1510 { get; set; }
        public bool Portaria671 { get; set; } = true;


    }
}
