using HP.Manager.DTOs.Endereco;
using HP.Manager.Validator.Endereco;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Empresa
{
    public record AtualizaEmpresaDto(
    int Id,
    EnderecoDto Endrereco,
    int Codigo,
    string RazaoSocial,
    string CnpjCpf,
    string Telefone,
    string TipoEmpresa,
    string? Site = null,
    string? Email = null,
    bool Portaria1510 = false,
    bool Portaria671 = true
);
}
