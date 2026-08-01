using HP.Manager.DTOs.Endereco;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Empresa
{
    public record EmpresaDto(
        int Id,
      
        int Codigo,
        string RazaoSocial,
        string CnpjCpf,
        string Telefone,
        string TipoEmpresa,
        DateTime DataCadastro,
        bool Portaria1510,
        EnderecoDto Endereco,
        string? Site = null,
        string? Email = null,
        DateTime? DataUltAtualizacao = null,
        bool Portaria671 = true
    );
}
