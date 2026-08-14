
namespace HP.Core.Entities
{
    public class Jornada
    {
        public int Id { get; set; }
        public int HorarioId { get; set; }
        public Horario Horario { get; set; } = null!;
        public DayOfWeek DiaSemana { get; set; }
        public TimeOnly Entrada1 { get; set; }
        public TimeOnly Saida1 { get; set; }
        public TimeOnly? Entrada2 { get; set; }
        public TimeOnly? Saida2 { get; set; }
        public TimeOnly? Entrada3 { get; set; }
        public TimeOnly? Saida3 { get; set; }

    }
}
