using HP.Core.Enums;
using HP.Manager.DTOs.Cargo;
using HP.Manager.DTOs.Endereco;
using HP.Manager.DTOs.EstruturaOrganizacional;

namespace HP.Manager.DTOs.Pessoa
{
    public record AtualizaPessoaDto(int Id, int EmpresaId,int Matricula, string Nome, DateTime? DataNascimento, DateTime DataAdmissao, DateTime? DataDemissao, EnderecoDto? Endereco, string? Rg, string Cpf, string Pis, string? Telefone, string? TelefoneCelular, string? Email, bool ControlaPonto, DateTime? DataControlaPonto, DateTime? DataNaoControlaPonto, Sexo Sexo, float BaseHoras, int? CargoId, int EstruturaId);
}
