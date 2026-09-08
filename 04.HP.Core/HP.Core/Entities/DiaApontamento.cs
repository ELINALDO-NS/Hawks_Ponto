using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class DiaApontamento
    {
        public long Id { get; set; }
        public DateTimeOffset? DataUltAtualizacao { get; set; }
        public DateOnly DataApontamento { get; set; }
        public int? PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
        public int MinutosTrabalhados { get; set; }
        public int MinutosAtraso { get; set; }
        public int MinutosFalta { get; set; }
        public int MinutosExtra { get; set; }
        public int MinutosCredito { get; set; }
        public int MinutosDebito { get; set; }
        public int MinutosAdicionalNoturno { get; set; }
        public int MinutosDSR { get; set; }
    }
}
