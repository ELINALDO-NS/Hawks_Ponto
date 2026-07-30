

namespace HP.Manager.DTOs.EstruturaOrganizacional
{
    public record EstruturaOrganizacionalDto(int Id,
    int Codigo,
    string Descricao,
    int? EstruturaPai,
    int EmpresaId,
    DateTime DataCadastro,
    DateTime? DataUltAtualizacao = null);
}
