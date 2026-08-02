using HP.Api.Configuration;
using HP.Manager.DTOs.Cargo;
using HP.Manager.Interfaces.Managers;

namespace HP.Api.Endpoints
{
    public static class CargoEndpoint
    {
        public static void MapCargoEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/Cargo").
                AddEndpointFilter<ValidationFilter<AdicionaCargoDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaCargoDto>>()
                .WithTags("Cargo");


            group.MapPost("/", async (AdicionaCargoDto novoCargo, ICargoManager manager, CancellationToken cancellationToken) =>
            {
                var cargo = await manager.AdicionarAsync(novoCargo, cancellationToken);
                return Results.Ok(cargo);
            }).Produces<CargoDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona um novo Cargo")
             .WithDescription("Recebe os dados cadastrais do Cargo");

            group.MapGet("/", async (ICargoManager manager, CancellationToken cancellationToken) =>
            {
                var cargos = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(cargos);
            }).Produces<CargoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todos os Cargos.")
             .WithDescription("Retorna uma lista contendo todos os Cargos cadastrados na Empresa. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, ICargoManager manager, CancellationToken cancellationToken) =>
            {
                var cargo = await manager.ObterPorIdAsync(id, cancellationToken);
                if (cargo is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(cargo);
            }).Produces<CargoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem um Cargo por id")
             .WithDescription("Retorna um Cargo específica com base no identificador único informado.");

            group.MapPut("/", async (AtualizaCargoDto cargo, ICargoManager manager, CancellationToken cancellationToken) =>
            {
                var cargoAtualizado = await manager.AtualizarAsync(cargo, cancellationToken);

                return Results.Ok(cargoAtualizado);
            }).Produces<CargoDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza um Cargo")
             .WithDescription("Altera as informações de um Cargo existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, ICargoManager manager, CancellationToken cancellationToken) =>
            {
                var cargo = await manager.RemoverAsync(id, cancellationToken);
                if (!cargo)
                {
                    return Results.NotFound(cargo);
                }
                return Results.Ok(cargo);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove um Cargo")
            .WithDescription("Exclui o registro de um Cargo do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
