using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Empresa
{
    public record EmpresaDto(
        int Id,
        int IdEndrereco,
        int Codigo,
        string RazaoSocial,
        string CnpjCpf,
        string Telefone,
        string TipoEmpresa,
        DateTime DataCadastro,
        bool Portaria1510,
        string? Site = null,
        string? Email = null,
        DateTime? DataUltAtualizacao = null,
        bool Portaria671 = true
    );
}
