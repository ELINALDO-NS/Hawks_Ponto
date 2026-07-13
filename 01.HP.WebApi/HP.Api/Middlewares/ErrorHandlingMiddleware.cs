using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Serilog;

namespace HP.Api.Middlewares;

public class ErrorHandlingMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // 1. Recupera ou gera o ID único da requisição (TraceId)
        var idErro = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        // 2. Trata erros de cancelamento (Ex: Usuário fechou o navegador antes da API responder)
        if (exception is OperationCanceledException)
        {
            Log.Warning("Erro [{IdErro}]: A requisição foi cancelada pelo usuário.", idErro);

            httpContext.Response.StatusCode = 499; // Client Closed Request

            // Se preferir retornar um JSON para o 499, descomente a linha abaixo:
            // await httpContext.Response.WriteAsJsonAsync(new { traceId = idErro, message = "Cancelado." }, cancellationToken);

            return true; // Avisa o .NET que o erro foi tratado
        }

        // 3. Loga qualquer outro erro crítico/não tratado com o Serilog
        Log.Error(exception, "Erro [{IdErro}]: Falha crítica capturada no servidor - {Message}", idErro, exception.Message);

        // 4. Monta a resposta JSON padronizada (RFC 7807 - ProblemDetails)
        var problema = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Ocorreu um erro interno no servidor",
            Detail = "Inconsistência interna. Use o código identificador (traceId) para suporte com o administrador.",
            Instance = httpContext.Request.Path
        };

        // Adiciona o ID do erro nas extensões do JSON para o cliente conseguir te informar depois
        problema.Extensions.Add("traceId", idErro);

        // 5. Configura e envia a resposta HTTP
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problema, cancellationToken);

        return true; // Retorna true para informar que a requisição foi encerrada com sucesso aqui
    }
}