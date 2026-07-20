using HP.Api.Configuration;
using HP.Api.Middlewares;
using HP.Data.Repository;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Implementation;
using HP.Manager.Interfaces.Managers;
using HP.Manager.Interfaces.Repository;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddExceptionHandler<ErrorHandlingMiddleware>();
builder.Services.AddProblemDetails();
builder.Services.AddSerilog();
builder.Services.AddSwaggerConfiguration(builder.Environment);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddScoped<IEmpresaRepository,EmpresaRepository>();
builder.Services.AddScoped<IEmpresaManager, EmpresaManager>();
// Add services to the container.

var app = builder.Build();
app.UseExceptionHandler();
app.UseCors();
app.UseDatabaseConfiguration();
app.UseSwaggerConfiguration();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/Empresa", async (AdicionaEmpresaDto novaempresa,IEmpresaManager manager,CancellationToken cancellationToken) =>
{
    var empresa = await manager.AdicionarAsync(novaempresa, cancellationToken);
    return Results.Ok(empresa); 
});
app.MapGet("/Empresa", async ( IEmpresaManager manager, CancellationToken cancellationToken) =>
{
    var empresas = await manager.ObterTodosAsync(cancellationToken);
    return Results.Ok(empresas);
});
app.MapPut("/Empresa", async (AtualizaEmpresaDto empresa, IEmpresaManager manager, CancellationToken cancellationToken) =>
{
    var empresaAtualizada = await manager.AtualizarAsync(empresa, cancellationToken);
    return Results.Ok(empresaAtualizada);
});
app.MapDelete("/Empresa", async (int id, IEmpresaManager manager, CancellationToken cancellationToken) =>
{
    var empresa = await manager.RemoverAsync(id, cancellationToken);
    return Results.Ok(empresa);
});


try
{
    Log.Information("Iniciando a aplicação...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush(); // Garante que todos os logs salvos de forma assíncrona sejam gravados antes de fechar
}
