using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class CargoPessoa
    {
        public int Id { get; set; }       
        public DateOnly DataInicio { get; set; } = new DateOnly();
        public DateOnly? DataFim { get; set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; } = null!;
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
