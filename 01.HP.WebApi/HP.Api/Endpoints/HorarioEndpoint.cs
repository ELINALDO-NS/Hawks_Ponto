using HP.Api.Configuration;
using HP.Manager.DTOs.Horario;
using HP.Manager.Interfaces;

namespace HP.Api.Endpoints
{
    public static class HorarioEndpoint
    {
        public static void MapHorarioEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/Horario").
                AddEndpointFilter<ValidationFilter<AdicionaHorarioDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaHorarioDto>>()
                .WithTags("Horario");


            group.MapPost("/", async (AdicionaHorarioDto novoHorario, IHorarioManager manager, CancellationToken cancellationToken) =>
            {
                var Horario = await manager.AdicionarAsync(novoHorario, cancellationToken);
                return Results.Ok(Horario);
            }).Produces<HorarioDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona um novo Horario")
             .WithDescription("Recebe os dados cadastrais do Horario ");

            group.MapGet("/", async (IHorarioManager manager, CancellationToken cancellationToken) =>
            {
                var Horarios = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(Horarios);
            }).Produces<HorarioDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todos os Horarios.")
             .WithDescription("Retorna uma lista contendo todos os Horarios cadastrados na Empresa. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IHorarioManager manager, CancellationToken cancellationToken) =>
            {
                var Horario = await manager.ObterPorIdAsync(id, cancellationToken);
                if (Horario is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(Horario);
            }).Produces<HorarioDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem um Horario por id")
             .WithDescription("Retorna um Horario específica com base no identificador único informado.");

            group.MapPut("/", async (AtualizaHorarioDto Horario, IHorarioManager manager, CancellationToken cancellationToken) =>
            {
                var HorarioAtualizado = await manager.AtualizarAsync(Horario, cancellationToken);

                return Results.Ok(HorarioAtualizado);
            }).Produces<HorarioDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza um Horario")
             .WithDescription("Altera as informações de um Horario existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, IHorarioManager manager, CancellationToken cancellationToken) =>
            {
                var Horario = await manager.RemoverAsync(id, cancellationToken);
                if (!Horario)
                {
                    return Results.NotFound(Horario);
                }
                return Results.Ok(Horario);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove um Horario")
            .WithDescription("Exclui o registro de um Horario do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
