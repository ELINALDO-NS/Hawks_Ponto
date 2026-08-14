
namespace HP.Core.Entities
{
    public class Horario
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descricao { get; set ; } = string.Empty;
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;        
        public ICollection<Jornada> Jornadas { get; set; } = new List<Jornada>();
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltAtualizacao { get; set; }
    }
}
