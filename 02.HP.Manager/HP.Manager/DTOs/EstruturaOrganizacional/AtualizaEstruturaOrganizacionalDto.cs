
namespace HP.Manager.DTOs.EstruturaOrganizacional
{
    public record AtualizaEstruturaOrganizacionalDto(int Id,
     int Codigo,
     string Descricao,
     int? EstruturaRelacionadaId,
     int EmpresaId
    );
}
