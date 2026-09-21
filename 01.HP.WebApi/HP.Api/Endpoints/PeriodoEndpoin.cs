using HP.Api.Configuration;
using HP.Manager.DTOs.Cargo;
using HP.Manager.DTOs.Periodo;
using HP.Manager.Interfaces;


namespace HP.Api.Endpoints
{
    public static class PeriodoEndpoin
    {
        public static void MapPeriodoEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/Periodo")
                .AddEndpointFilter<ValidationFilter<FechaPeriodoDto>>()
                .AddEndpointFilter<ValidationFilter<ReabriPeridoDto>>()
                .WithTags("Periodo");


            group.MapPost("/Fechar", async (FechaPeriodoDto fechaPeriodo, IPeriodoManager manager, CancellationToken cancellationToken) =>
            {
                var periodo = await manager.FecharAsync(fechaPeriodo, cancellationToken);
                return Results.Ok(periodo);
            }).Produces<CargoDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona um fechamento de perido")
             .WithDescription("Adiciona um fechamento de periodo");

            group.MapGet("/", async (IPeriodoManager manager, CancellationToken cancellationToken) =>
            {
                var periodos = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(periodos);
            }).Produces<List<PeriodoDto>>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todos os Periodos.")
             .WithDescription("Retorna uma lista contendo todos os Periodos cadastrados na Empresa. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IPeriodoManager manager, CancellationToken cancellationToken) =>
            {
                var periodo = await manager.ObterPorIdAsync(id, cancellationToken);
                if (periodo is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(periodo);
            }).Produces<PeriodoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem um periodo por id")
             .WithDescription("Retorna um periodo específico com base no identificador único informado.");

            group.MapPost("/Reabrir", async (ReabriPeridoDto periodoDto, IPeriodoManager manager, CancellationToken cancellationToken) =>
            {
                var periodo = await manager.ReabrirAsync(periodoDto, cancellationToken);

                return Results.Ok(periodo);
            })
               .Produces<PeriodoDto>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithSummary("Reabre um periodo fechado")
              .WithDescription("Reabre um periodo fechado específico com base no identificador único informado.");


            group.MapDelete("/{id:int}", async (int id, IPeriodoManager manager, CancellationToken cancellationToken) =>
            {
                var periodo = await manager.RemoverAsync(id, cancellationToken);
                if (!periodo)
                {
                    return Results.NotFound(periodo);
                }
                return Results.Ok(periodo);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove um Periodo")
            .WithDescription("Exclui o registro de um perido do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
