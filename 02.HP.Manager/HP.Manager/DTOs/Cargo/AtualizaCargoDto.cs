using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Cargo
{
    public record AtualizaCargoDto(int Id, int Codigo, string Descricao, int EmpresaId);

}
