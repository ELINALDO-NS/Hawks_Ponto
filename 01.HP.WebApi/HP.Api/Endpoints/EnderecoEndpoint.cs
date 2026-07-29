using HP.Api.Configuration;
using HP.Manager.DTOs.Endereco;
using HP.Manager.Interfaces.Managers;

namespace HP.Api.Endpoints
{
    public static class EnderecoEndpoint
    {
        public static void MapEnderecoEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/endereco").
                AddEndpointFilter<ValidationFilter<AdicionaEnderecoDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaEnderecoDto>>()
                .WithTags("Endereco");


            group.MapPost("/", async (AdicionaEnderecoDto novoendereco, IEnderecoManager manager, CancellationToken cancellationToken) =>
            {
                var endereco = await manager.AdicionarAsync(novoendereco, cancellationToken);
                return Results.Ok(endereco);
            }).Produces<EnderecoDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona um novo edereco")
             .WithDescription("");

            group.MapGet("/", async (IEnderecoManager manager, CancellationToken cancellationToken) =>
            {
                var enderecos = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(enderecos);
            }).Produces<EnderecoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todos os endereco.")
             .WithDescription("Retorna uma lista contendo todos os enderecos cadastrados no sistema. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IEnderecoManager manager, CancellationToken cancellationToken) =>
            {
                var endereco = await manager.ObterPorIdAsync(id, cancellationToken);
                if (endereco is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(endereco);
            }).Produces<EnderecoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem um endereco por id")
             .WithDescription("Retorna um endereco específico com base no identificador único informado.");

            group.MapPut("/", async (AtualizaEnderecoDto endereco, IEnderecoManager manager, CancellationToken cancellationToken) =>
            {
                var enderecoAtualizado = await manager.AtualizarAsync(endereco, cancellationToken);

                return Results.Ok(enderecoAtualizado);
            }).Produces<EnderecoDto>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza um endereco")
             .WithDescription("Altera as informações de um endereco existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, IEnderecoManager manager, CancellationToken cancellationToken) =>
            {
                var excluido = await manager.RemoverAsync(id, cancellationToken);
                if (!excluido)
                {
                    return Results.NotFound(excluido);
                }
                return Results.Ok(excluido);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove um endereco")
            .WithDescription("Exclui o registro de um endereco do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
