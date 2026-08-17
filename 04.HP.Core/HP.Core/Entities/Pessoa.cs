
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
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltAtualizacao { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public string? Rg { get; set; }
        public string Cpf { get; set; } = null!;
        public string Pis { get; set; } = null!;
        public string? Telefone { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? Email { get; set; }
        public bool ControlaPonto { get; set; } = true;
        public DateTime? DataControlaPonto { get; set; } = DateTime.UtcNow;
        public DateTime? DataNaoControlaPonto { get; set; }
        public Sexo Sexo { get; set; }
        public float BaseHoras { get; set; }
        public int EstruturaId { get; set; }
        public EstruturaOrganizacional Estrutura { get; set; } = null!;
        public int? CargoId
        {   get;
            set => field = value == 0 ? null : value;
        } = null;
        public Cargo Cargo { get; set; } = null!;

    }
}
