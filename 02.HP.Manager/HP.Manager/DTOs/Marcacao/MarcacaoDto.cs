using HP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Marcacao
{
    public record MarcacaoDto(long Id, DateTimeOffset DataPersistencia, DateTimeOffset DataHora, int? RelogioId, string CPF, TipoMarcacao TipoMarcacao, long? NSR, OrigemMarcacao OrigemMarcacao, string? Justificativa);
    
    
    
}
