using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var idErro = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        if (exception is OperationCanceledException)
        {
            logger.LogWarning("Erro [{IdErro}]: A requisição foi cancelada pelo usuário.", idErro);
            httpContext.Response.StatusCode = 499;
            return true;
        }

        if (exception is ValidationException validationException)
        {
            logger.LogWarning("Erro [{IdErro}]: Falha de validação - {Message}", idErro, exception.Message);

            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var validationProblem = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Um ou mais erros ocorreram",
                Instance = httpContext.Request.Path
            };
            validationProblem.Extensions.Add("traceId", idErro);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(validationProblem, cancellationToken);

            return true;
        }

        if (exception is BadHttpRequestException badRequestException)
        {
            logger.LogWarning("Erro [{IdErro}]: JSON inválido na requisição - {Message}", idErro, badRequestException.Message);

            await WriteProblemAsync(
                httpContext,
                StatusCodes.Status400BadRequest,
                "Requisição mal formada",
                "O corpo da requisição contém um JSON inválido. Verifique a sintaxe e os tipos dos campos enviados.",
                idErro,
                cancellationToken);

            return true;
        }

        
        var (statusCode, title, logAsWarning) = exception switch
        {
            DbUpdateConcurrencyException => (StatusCodes.Status409Conflict, "Conflito de concorrência ao atualizar o recurso", true),
            DbUpdateException => (StatusCodes.Status409Conflict, "Erro ao persistir os dados. Verifique restrições de integridade", true),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Acesso negado", true),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado", true),
            NotSupportedException => (StatusCodes.Status400BadRequest, "Operação não suportada", true),
            TimeoutException => (StatusCodes.Status504GatewayTimeout, "Tempo limite excedido ao processar a requisição", true),
            _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor", false)
        };

        if (logAsWarning)
        {
            logger.LogWarning(exception, "Erro [{IdErro}]: {Title} - {Message}", idErro, title, exception.Message);
        }
        else
        {
            logger.LogError(exception, "Erro [{IdErro}]: Falha crítica capturada no servidor - {Message}", idErro, exception.Message);
        }

        var detail = statusCode == StatusCodes.Status500InternalServerError
            ? "Inconsistência interna. Use o código identificador (traceId) para suporte com o administrador."
            : exception.Message;

        await WriteProblemAsync(httpContext, statusCode, title, detail, idErro, cancellationToken);

        return true;
    }

    private static async Task WriteProblemAsync(
        HttpContext httpContext,
        int statusCode,
        string title,
        string detail,
        string traceId,
        CancellationToken cancellationToken)
    {
        var problema = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };
        problema.Extensions.Add("traceId", traceId);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problema, cancellationToken);
    }
}