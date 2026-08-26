using HP.Core.Enums;
using HP.Manager.DTOs.Endereco;


namespace HP.Manager.DTOs.Pessoa
{
    public record AdicionaPessoaDto(int EmpresaId, int Matricula, string Nome, DateTimeOffset? DataNascimento, DateTimeOffset DataAdmissao, DateTimeOffset? DataDemissao, EnderecoDto? Endereco, string? Rg, string Cpf, string Pis, string? Telefone, string? TelefoneCelular, string? Email, bool ControlaPonto, DateTimeOffset? DataControlaPonto, DateTimeOffset? DataNaoControlaPonto, Sexo Sexo, float BaseHoras,AdicionaHorarioPessoaDto Horario, AdicionaCargoPessoaDto? Cargo, AdicionaEstruturaOrganizacionalPessoaDto EstruturaOrganizacional);

}
