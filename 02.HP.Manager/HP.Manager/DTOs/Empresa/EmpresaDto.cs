using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Empresa
{
    public class EmpresaDto
    {
        public EmpresaDto()
        {
                
        }
        public EmpresaDto(int id, int idEndrereco, int codigo, string razaoSocial, string cnpjCpf, string telefne, string? site, string? email, string tipoEmpresa, DateTime dataCadastro, DateTime? dataUltAtualizacao, bool portaria1510, bool portaria671)
        {
            Id = id;
            IdEndrereco = idEndrereco;
            Codigo = codigo;
            RazaoSocial = razaoSocial;
            CnpjCpf = cnpjCpf;
            Telefone = telefne;
            Site = site;
            Email = email;
            TipoEmpresa = tipoEmpresa;
            DataCadastro = dataCadastro;
            DataUltAtualizacao = dataUltAtualizacao;
            Portaria1510 = portaria1510;
            Portaria671 = portaria671;
            
        }

        public int Id { get; set; }
        public int IdEndrereco { get; set; }
        public int Codigo { get; set; }
        public string RazaoSocial { get; set; } = null!;
        public string CnpjCpf { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string? Site { get; set; }
        public string? Email { get; set; }
        public string TipoEmpresa { get; set; } = null!;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltAtualizacao { get; set; }
        public bool Portaria1510 { get; set; }
        public bool Portaria671 { get; set; } = true;
        
    }
}
