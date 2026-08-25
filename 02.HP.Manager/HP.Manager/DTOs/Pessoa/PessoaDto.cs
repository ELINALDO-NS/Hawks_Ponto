using HP.Core.Enums;
using HP.Manager.DTOs.Endereco;


namespace HP.Manager.DTOs.Pessoa
{
    public record PessoaDto(int EmpresaId, int Id, int Matricula, string Nome, DateTime? DataNascimento, DateTime DataAdmissao, DateTime? DataDemissao, EnderecoDto? Endereco, string? Rg, string Cpf, string Pis, string? Telefone, string? TelefoneCelular, string? Email, bool ControlaPonto, DateTime? DataControlaPonto, DateTime? DataNaoControlaPonto, Sexo Sexo, float BaseHoras,List<HorarioPessoaDto> Horario, List<EstruturaOrganizacionalPessoaDto> EstruturaOrganizacional, List<CargoPessoaDto> Cargo);

}
