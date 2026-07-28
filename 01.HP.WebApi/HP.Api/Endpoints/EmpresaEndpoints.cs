using HP.Api.Configuration;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Interfaces.Managers;

namespace HP.Api.Endpoints
{
    public static class EmpresaEndpoints
    {
        public static void MapEmpresaEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/empresa").
                AddEndpointFilter<ValidationFilter<AdicionaEmpresaDto>>().
                AddEndpointFilter<ValidationFilter<AtualizaEmpresaDto>>()
                .WithTags("Empresa");
            

            group.MapPost("/", async (AdicionaEmpresaDto novaempresa, IEmpresaManager manager, CancellationToken cancellationToken) =>
            {
                var empresa = await manager.AdicionarAsync(novaempresa, cancellationToken);
                return Results.Ok(empresa);
            }).AddEndpointFilter<ValidationFilter<EmpresaDto>>()
            .Produces<EmpresaDto>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status500InternalServerError)
             .WithSummary("Adiciona uma nova empresa")
             .WithDescription("Recebe os dados cadastrais da empresa, " +
               "valida o CNPJ e insere o registro na base de dados principal.");

            group.MapGet("/", async (IEmpresaManager manager, CancellationToken cancellationToken) =>
            {
                var empresas = await manager.ObterTodosAsync(cancellationToken);
                return Results.Ok(empresas);
            }).Produces<EmpresaDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status204NoContent)
             .WithSummary("Obtem todas as empresas.")
             .WithDescription("Retorna uma lista contendo todas as empresas cadastradas no sistema. " +
             "Caso não existam registros, retorna uma lista vazia.");

            group.MapGet("/{id:int}", async (int id, IEmpresaManager manager, CancellationToken cancellationToken) =>
            {
                var empresa = await manager.ObterPorIdAsync(id, cancellationToken);
                if (empresa is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(empresa);
            }).Produces<EmpresaDto>(StatusCodes.Status200OK).
               Produces(StatusCodes.Status404NotFound)
              .WithSummary("Obtem uma empresas por id")
             .WithDescription("Retorna uma empresa específica com base no identificador único informado.");

            group.MapPut("/", async (AtualizaEmpresaDto empresa, IEmpresaManager manager, CancellationToken cancellationToken) =>
            {
                var empresaAtualizada = await manager.AtualizarAsync(empresa, cancellationToken);

                return Results.Ok(empresaAtualizada);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza uma empresa")
             .WithDescription("Altera as informações de uma empresa existente " +
             "com base nos dados fornecidos no corpo da requisição.");

            group.MapDelete("/{id:int}", async (int id, IEmpresaManager manager, CancellationToken cancellationToken) =>
            {
                var empresa = await manager.RemoverAsync(id, cancellationToken);
                if (!empresa)
                {
                    return Results.NotFound(empresa);
                }
                return Results.Ok(empresa);
            }).Produces<bool>(StatusCodes.Status200OK)
            .Produces<bool>(StatusCodes.Status404NotFound)
            .WithSummary("Remove uma empresa")
            .WithDescription("Exclui o registro de uma empresa do sistema permanentemente " +
            "a partir do seu identificador único.");
             
        }
    }
}
