
namespace HP.Manager.DTOs.EstruturaOrganizacional
{
    public record AdicionaEstruturaOrganizacionalDto(
    int Codigo,
    string Descricao,
    int? EstruturaRelacionadaId,
    int EmpresaId);
}
