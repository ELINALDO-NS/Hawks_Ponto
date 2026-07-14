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

        var idErro = Activity.Current?.Id ?? httpContext.TraceIdentifier;


        if (exception is OperationCanceledException)
        {
            Log.Warning("Erro [{IdErro}]: A requisição foi cancelada pelo usuário.", idErro);

            httpContext.Response.StatusCode = 499;


            return true;
        }


        Log.Error(exception, "Erro [{IdErro}]: Falha crítica capturada no servidor - {Message}", idErro, exception.Message);


        var problema = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Ocorreu um erro interno no servidor",
            Detail = "Inconsistência interna. Use o código identificador (traceId) para suporte com o administrador.",
            Instance = httpContext.Request.Path
        };


        problema.Extensions.Add("traceId", idErro);


        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problema, cancellationToken);

        return true;
    }
}