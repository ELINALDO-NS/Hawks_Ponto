
namespace HP.Manager.DTOs.Jornada
{
    public record JornadaDto(int Id, DayOfWeek DiaSemana, TimeOnly Entrada1, TimeOnly Saida1, TimeOnly? Entrada2, TimeOnly? Saida2, TimeOnly? Entrada3, TimeOnly? Saida3);
}
