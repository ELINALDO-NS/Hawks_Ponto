using HP.Api.Configuration;
using HP.Manager.DTOs.EstruturaOrganizacional;
using HP.Manager.Interfaces.Managers;

namespace HP.Api.Endpoints
{
    public static class EstruturaOrganizacionalEndpoint
    {
        public static void MapEstruturaOrganizacionalEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/EstruturaOrganizacional").
                AddEndpointFilter<ValidationFilter<AdicionaEstruturaOrganizacionalDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaEstruturaOrganizacionalDto>>()
                .WithTags("Estrutura Organizacional");


            group.MapPost("/", async (AdicionaEstruturaOrganizacionalDto novaEstrutura, IEstruturaOrganizacionalManager manager, CancellationToken cancellationToken) =>
            {
                var estrutura = await manager.AdicionarAsync(novaEstrutura, cancellationToken);
                return Results.Ok(estrutura);
            }).Produces<EstruturaOrganizacionalDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona uma nova Estrutura Organizacional")
             .WithDescription("Recebe os dados cadastrais da Estrutura Organizacional");

            group.MapGet("/", async (IEstruturaOrganizacionalManager manager, CancellationToken cancellationToken) =>
            {
                var estruturas = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(estruturas);
            }).Produces<EstruturaOrganizacionalDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todas as Estrutura Organizacional.")
             .WithDescription("Retorna uma lista contendo todas as Estrutura Organizacional cadastradas na Empresa. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IEstruturaOrganizacionalManager manager, CancellationToken cancellationToken) =>
            {
                var estrutura = await manager.ObterPorIdAsync(id, cancellationToken);
                if (estrutura is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(estrutura);
            }).Produces<EstruturaOrganizacionalDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem uma Estrutura Organizacional por id")
             .WithDescription("Retorna uma Estrutura Organizacional específica com base no identificador único informado.");

            group.MapPut("/", async (AtualizaEstruturaOrganizacionalDto estrutura, IEstruturaOrganizacionalManager manager, CancellationToken cancellationToken) =>
            {
                var estruturaAtualizada = await manager.AtualizarAsync(estrutura, cancellationToken);

                return Results.Ok(estruturaAtualizada);
            }).Produces<EstruturaOrganizacionalDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza uma Estrutura Organizacional")
             .WithDescription("Altera as informações de uma Estrutura Organizacional existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, IEstruturaOrganizacionalManager manager, CancellationToken cancellationToken) =>
            {
                var estrutura = await manager.RemoverAsync(id, cancellationToken);
                if (!estrutura)
                {
                    return Results.NotFound(estrutura);
                }
                return Results.Ok(estrutura);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove uma Estrutura Organizacional")
            .WithDescription("Exclui o registro de uma Estrutura Organizacional do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
