

namespace HP.Manager.DTOs.EstruturaOrganizacional
{
    public record EstruturaOrganizacionalDto(int Id,
    int Codigo,
    string Descricao,
    int? EstruturaPai,
    int EmpresaId,
    DateTimeOffset DataCadastro,
    DateTimeOffset? DataUltAtualizacao = null);
}
