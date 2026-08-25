using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class HorarioPessoa
    {
        public int Id { get; set; }
        public DateOnly DataInicio { get; set; } = new DateOnly();
        public DateOnly? DataFim { get; set; }
        public int HorarioId { get; set; }
        public Horario Horario { get; set; } = null!;
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
        public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now.ToLocalTime();
    }
}
