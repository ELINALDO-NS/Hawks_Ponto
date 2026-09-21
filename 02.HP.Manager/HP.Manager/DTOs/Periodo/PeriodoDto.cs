using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Periodo
{
    public record PeriodoDto(int Id, DateOnly DataInicio, DateOnly DataFim, bool Aberto, DateTimeOffset? DataUltAtualizacao);
}
