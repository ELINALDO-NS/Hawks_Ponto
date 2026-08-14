using HP.Manager.DTOs.Jornada;

namespace HP.Manager.DTOs.Horario
{
    public record AtualizaHorarioDto(int Id, int EmpresaId, string Codigo, string Descricao,
        ICollection<JornadaDto> Jornadas);

}
