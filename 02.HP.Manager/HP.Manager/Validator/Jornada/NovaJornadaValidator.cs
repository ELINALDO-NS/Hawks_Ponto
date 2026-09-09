using FluentValidation;
using HP.Manager.DTOs.Jornada;


namespace HP.Manager.Validator.Jornada
{
    public class NovaJornadaValidator : AbstractValidator<JornadaDto>
    {
        public NovaJornadaValidator()
        {
            RuleFor(x => x.DiaSemana)
                .IsInEnum().WithMessage("O dia da semana informado é inválido.");

            RuleFor(x => x.Entrada1).NotEmpty().WithMessage("A Entrada 1 é obrigatória.");
            RuleFor(x => x.Saida1).NotEmpty().WithMessage("A Saída 1 é obrigatória.");

            When(x => x.Entrada2.HasValue || x.Saida2.HasValue, () =>
            {
                RuleFor(x => x.Entrada2).NotNull().WithMessage("A Entrada 2 é obrigatória quando a Saída 2 for informada.");
                RuleFor(x => x.Saida2).NotNull().WithMessage("A Saída 2 é obrigatória quando a Entrada 2 for informada.");
            });

            When(x => x.Entrada3.HasValue || x.Saida3.HasValue, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.Entrada2.HasValue && x.Saida2.HasValue)
                    .WithMessage("Não é possível informar o 3º turno sem preencher o 2º turno completamente.");

                RuleFor(x => x.Entrada3).NotNull().WithMessage("A Entrada 3 é obrigatória quando a Saída 3 for informada.");
                RuleFor(x => x.Saida3).NotNull().WithMessage("A Saída 3 é obrigatória quando a Entrada 3 for informada.");
            });

            
            RuleFor(x => x)
                .Must(SequenciaCronologicaValida)
                .WithMessage("Os horários devem estar em ordem cronológica (turnos noturnos podem virar o dia).");


        }


        private static bool SequenciaCronologicaValida(JornadaDto jornada)
        {
            var horarios = new List<TimeOnly?>
              {
                  jornada.Entrada1, jornada.Saida1,
                  jornada.Entrada2, jornada.Saida2,
                  jornada.Entrada3, jornada.Saida3
              };

            int diasAcumulados = 0;
            TimeOnly? anterior = null;
            int? valorAnteriorAcumulado = null;

            foreach (var horario in horarios)
            {
                if (horario is null) continue; 

                if (anterior.HasValue && horario.Value < anterior.Value)
                    diasAcumulados++;

                var valorAcumulado = horario.Value.Hour * 60 + horario.Value.Minute + diasAcumulados * 24 * 60;

                if (valorAnteriorAcumulado.HasValue && valorAcumulado <= valorAnteriorAcumulado.Value)
                    return false;

                valorAnteriorAcumulado = valorAcumulado;
                anterior = horario;
            }

            return true;
        }

    }
}
