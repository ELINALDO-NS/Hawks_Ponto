using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Periodo
{
    public record FechaPeriodoDto(int Id, DateOnly DataInicio, DateOnly DataFim);

}
