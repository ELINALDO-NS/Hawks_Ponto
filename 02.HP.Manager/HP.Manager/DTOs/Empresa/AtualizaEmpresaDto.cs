using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Empresa
{
    public class AtualizaEmpresaDto
    {
        public int Id { get; set; }
        public int IdEndrereco { get; set; }
        public int Codigo { get; set; }
        public string RazaoSocial { get; set; } = null!;
        public string CnpjCpf { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string? Site { get; set; }
        public string? Email { get; set; }
        public string TipoEmpresa { get; set; } = null!;
        public bool Portaria1510 { get; set; } = false;
        public bool Portaria671 { get; set; } = true;
        
    }
}
