using HP.Core.Enums;

namespace HP.Manager.DTOs.Marcacao
{
    public record AdicionaMarcacaoDto(DateTimeOffset DataHora, int? RelogioId, string CPF, TipoMarcacao TipoMarcacao, long? NSR, OrigemMarcacao OrigemMarcacao, string? Justificativa);
}
