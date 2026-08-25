
using HP.Core.Enums;
using HP.Core.Extentions;

namespace HP.Core.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;
        public int Matricula { get; set; }
        public string Nome
        {
            get; set => field = value.ToTitleCase() ?? string.Empty;
        } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now.ToLocalTime();
        public DateTimeOffset? DataUltAtualizacao { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public string? Rg { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Pis { get; set; }
        public string? Telefone { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? Email { get; set; }
        public bool ControlaPonto { get; set; } = true;
        public DateTime? DataControlaPonto { get; set; } = DateTime.UtcNow;
        public DateTime? DataNaoControlaPonto { get; set; }
        public Sexo Sexo { get; set; }
        public float BaseHoras { get; set; }
        public ICollection<HorarioPessoa> Horarios { get; set; } = null!;
        public ICollection<EstruturaOrganizacionalPessoa> EstruturasOrganizacionais { get; set; } = null!;
        public ICollection<CargoPessoa> Cargos { get; set; } = new List<CargoPessoa>();

    }
}
