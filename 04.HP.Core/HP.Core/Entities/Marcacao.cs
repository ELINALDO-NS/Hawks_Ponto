
using HP.Core.Enums;

namespace HP.Core.Entities
{
    public class Marcacao
    {
        public long Id { get; set; }
        public DateTimeOffset DataPersistencia { get; set; } = DateTimeOffset.Now.ToLocalTime();

        public int? RelogioId { get; set; }
        public TipoMarcacao TipoMarcacao { get; set; }
        public long? NSR { get; set; }
        public DateTimeOffset DataHora { get; set; }
        public string CPF { get; set; } = null!;
        public string PIS { get; set; } = null!;
        public OrigemMarcacao OrigemMarcacao { get; set; }
        public string? Justificativa { get; set; }
    }
}
