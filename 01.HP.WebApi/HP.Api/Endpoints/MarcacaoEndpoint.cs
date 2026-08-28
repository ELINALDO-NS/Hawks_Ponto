using HP.Api.Configuration;
using HP.Manager.DTOs.Marcacao;
using HP.Manager.Interfaces;
namespace HP.Api.Endpoints
{
    public static class MarcacaoEndpoint
    {
        public static void MapMarcacaoEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/Marcacao").
                AddEndpointFilter<ValidationFilter<AdicionaMarcacaoDto>>()
                .WithTags("Marcacao");


            group.MapPost("/", async (AdicionaMarcacaoDto novaMarcacao, IMarcacaoManager manager, CancellationToken cancellationToken) =>
            {
                var marcacao = await manager.AdicionarAsync(novaMarcacao, cancellationToken);
                return Results.Ok(marcacao);
            }).Produces<MarcacaoDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona uma nova Marcação")
             .WithDescription("Recebe os dados cadastrais da Marcação");

            group.MapPost("/Marcacoes", async (ObterPorCpfEPeriodoDto obterPorCpf, IMarcacaoManager manager, CancellationToken cancellationToken) =>
            {
                var Marcacoes = await manager.ObterPorCpfEPeriodoAsync(obterPorCpf, cancellationToken);
                return Results.Ok(Marcacoes);
            }).Produces<List<MarcacaoDto>>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem Marcações de uma pessoa por periodo")
             .WithDescription("Retorna uma lista contendo todas as Marcaçoes no periodo informado");

            group.MapGet("/{id:int}", async (long id, IMarcacaoManager manager, CancellationToken cancellationToken) =>
            {
                var marcacaoes = await manager.ObterPorIdAsync(id, cancellationToken);
                if (marcacaoes is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(marcacaoes);
            }).Produces<MarcacaoDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem uma marcação por id")
             .WithDescription("Retorna uma marcação específica com base no identificador único informado.");
        }
    }
}
