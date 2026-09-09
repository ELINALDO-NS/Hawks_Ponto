using HP.Manager.DTOs.Jornada;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Horario
{
    public record HorarioDto(int Id, int EmpresaId, string Codigo, string Descricao, TimeOnly InicioFimDia,
        ICollection<JornadaDto> Jornadas, DateTimeOffset DataCadastro, DateTimeOffset? DataUltAtualizacao);
    
}
