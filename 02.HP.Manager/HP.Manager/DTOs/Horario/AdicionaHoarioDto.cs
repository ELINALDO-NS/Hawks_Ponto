using HP.Manager.DTOs.Jornada;

namespace HP.Manager.DTOs.Horario
{
    public record AdicionaHorarioDto(int EmpresaId, string Codigo, string Descricao, TimeOnly InicioFimDia,
        ICollection<JornadaDto> Jornadas);

}
