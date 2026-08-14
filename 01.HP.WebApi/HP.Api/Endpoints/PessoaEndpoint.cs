using HP.Api.Configuration;
using HP.Manager.DTOs.Pessoa;
using HP.Manager.Interfaces;

namespace HP.Api.Endpoints
{
    public static class PessoaEndpoint
    {
        public static void MapPessoaEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/Pessoa").
                AddEndpointFilter<ValidationFilter<AdicionaPessoaDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaPessoaDto>>()
                .WithTags("Pessoa");


            group.MapPost("/", async (AdicionaPessoaDto novapessoa, IPessoaManager manager, CancellationToken cancellationToken) =>
            {
                var pessoa = await manager.AdicionarAsync(novapessoa, cancellationToken);
                return Results.Ok(pessoa);
            }).Produces<PessoaDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona uma nova Pessoa")
             .WithDescription("Recebe os dados cadastrais da Pessoa");

            group.MapGet("/", async (IPessoaManager manager, CancellationToken cancellationToken) =>
            {
                var pessoas = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(pessoas);
            }).Produces<PessoaDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todas as Pessoas.")
             .WithDescription("Retorna uma lista contendo todas as Pessoas cadastrados na Empresa. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IPessoaManager manager, CancellationToken cancellationToken) =>
            {
                var pessoa = await manager.ObterPorIdAsync(id, cancellationToken);
                if (pessoa is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(pessoa);
            }).Produces<PessoaDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem uma Pessoa por id")
             .WithDescription("Retorna uma PEssoa específica com base no identificador único informado.");

            group.MapPut("/", async (AtualizaPessoaDto pessoa, IPessoaManager manager, CancellationToken cancellationToken) =>
            {
                var PessoaAtualizada = await manager.AtualizarAsync(pessoa, cancellationToken);
                if (PessoaAtualizada is not null)
                {
                    return Results.Ok(PessoaAtualizada);
                }
                return Results.NotFound("Pessoa não encontrada");

                
            }).Produces<PessoaDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza uma Pessoa")
             .WithDescription("Altera as informações de um Pessoa existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, IPessoaManager manager, CancellationToken cancellationToken) =>
            {
                var pessoa = await manager.RemoverAsync(id, cancellationToken);
                if (!pessoa)
                {
                    return Results.NotFound(pessoa);
                }
                return Results.Ok(pessoa);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove uma Pessoa")
            .WithDescription("Exclui o registro de uma Pessoa do sistema permanentemente " +
            "a partir do seu identificador único.");

        }

    }
}
