using HP.Core.Extentions;
using HP.Manager.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Cargo
{
    public record CargoDto(int Id, int Codigo, string Descricao, int EmpresaId, DateTime DataCadastro, DateTime? DataUltAtualizacao);


}
