
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
        public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now.ToLocalTime();
        public DateTimeOffset? DataUltAtualizacao { get; set; }
    }
}
