using HP.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao
        {
            get; set => field = value.ToTitleCase() ?? string.Empty;
        } = string.Empty;
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltAtualizacao { get; set; }
    }
}
