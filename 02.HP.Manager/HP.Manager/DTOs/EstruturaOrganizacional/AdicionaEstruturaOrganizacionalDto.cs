
namespace HP.Manager.DTOs.EstruturaOrganizacional
{
    public record AdicionaEstruturaOrganizacionalDto(
    int Codigo,
    string Descricao,
    int EmpresaId,
    int? EstruturaRelacionadaId = null);
}
