using HP.Api.Configuration;
using HP.Api.Endpoints;
using HP.Api.Middlewares;
using HP.Data.Repository;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Implementation;
using HP.Manager.Interfaces.Managers;
using HP.Manager.Interfaces.Repository;
using Mapster;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();


var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddMapster();

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
builder.Services.UseDependencyInjectionConfiguration();
// Add services to the container.

var app = builder.Build();
app.UseExceptionHandler();
app.UseCors();
app.UseDatabaseConfiguration();
app.UseSwaggerConfiguration();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//Endpoints
app.MapEmpresaEndpoints();

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
    Log.CloseAndFlush(); 
}
