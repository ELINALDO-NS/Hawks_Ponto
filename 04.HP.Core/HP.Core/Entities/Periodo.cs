using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class Periodo
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; } = new DateOnly(2099,12,31);
        public bool Aberto { get; set; }
        public DateTimeOffset? DataUltAtualizacao { get; set; }
    }
}
