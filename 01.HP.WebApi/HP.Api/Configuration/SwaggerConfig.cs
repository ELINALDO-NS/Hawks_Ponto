using HP.Core.Enums;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HP.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IHostEnvironment env)
        {
            var isEfTool = AppDomain.CurrentDomain.FriendlyName.Contains("ef", StringComparison.OrdinalIgnoreCase);

            if (env.IsDevelopment() && !isEfTool)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Hawks Ponto",
                        Version = "v1",
                        Description = "API da aplicação Hawks Ponto",
                        Contact = new OpenApiContact
                        {
                            Name = "Elinaldo Nascimeto",
                            Email = "elinaldo_nascimento@Outlook.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "OSD",
                            Url = new System.Uri("https://opensource.org/osd")
                        },
                        TermsOfService = new System.Uri("https://opensource.org/osd")

                    });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Insira o token JWT desta forma: Bearer {seu_token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    });

                    options.AddSecurityRequirement(document =>
                    {
                        var schemeRef = new OpenApiSecuritySchemeReference("Bearer", document);

                        return new OpenApiSecurityRequirement
                        {
                            [schemeRef] = Array.Empty<string>().ToList()
                        };
                    });
                    options.SchemaFilter<DateTimeOffsetSchemaFilter>();
                    options.SchemaFilter<TimeOnlySchemaFilter>();
                    options.SchemaFilter<DayOfWeekSchemaFilter>();
                    options.SchemaFilter<EnumSexoSchemaFilter>();
                    options.SchemaFilter<EnumTipoEmpresaSchemaFilter>();
                    options.SchemaFilter<EnumOrigemMarcacaoSchemaFilter>();
                    options.SchemaFilter<EnumTipoMarcacaoSchemaFilter>();

                });
            }
        }
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}

public class DayOfWeekSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<int, string> DiasEmPortugues = new()
    {
        [0] = "Sunday",
        [1] = "Monday",
        [2] = "Tuesday",
        [3] = "Wednesday",
        [4] = "Thursday",
        [5] = "Friday",
        [6] = "Saturday"
    };

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(DayOfWeek))
        {
            var legenda = string.Join(", ", DiasEmPortugues.Select(x => $"{x.Key} = {x.Value}"));
            concreteSchema.Description = $"Dia da semana: {legenda}.";
        }
    }
}
public class DateTimeOffsetSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(DateTimeOffset) || context.Type == typeof(DateTimeOffset?))
        {
            concreteSchema.Type = JsonSchemaType.String;
            concreteSchema.Format = "date-time";
            concreteSchema.Example = "2026-08-21T14:19:53-03:00";
        }
    }
}
public class TimeOnlySchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(TimeOnly) || context.Type == typeof(TimeOnly?))
        {
            concreteSchema.Type = JsonSchemaType.String;
            concreteSchema.Format = "time";
            concreteSchema.Example = "11:29:00";
        }
    }
}
public class EnumSexoSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<int, string> Sexo = new()
    {
        
        [1] = "Masculino",
        [2] = "Feminino",
        [3] = "Outro"
       
    };

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(Sexo))
        {
            var legenda = string.Join(", ", Sexo.Select(x => $"{x.Key} = {x.Value}"));
            concreteSchema.Description = $"Sexo: {legenda}.";
        }
    }
}
public class EnumOrigemMarcacaoSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<int, string> Sexo = new()
    {
        
        [1] = "REPC",
        [2] = "REPP",
        [3] = "REPA",
        [4] = "ImportacaoTxt",
        [5] = "Manual"

    };

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(OrigemMarcacao))
        {
            var legenda = string.Join(", ", Sexo.Select(x => $"{x.Key} = {x.Value}"));
            concreteSchema.Description = $"Origem Marcacao: {legenda}.";
        }
    }
}
public class EnumTipoMarcacaoSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<int, string> Sexo = new()
    {

        [0] = "Original",
        [1] = "Editada",
        [2] = "Indevida",
        [3] = "Automatica"

    };

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(TipoMarcacao))
        {
            var legenda = string.Join(", ", Sexo.Select(x => $"{x.Key} = {x.Value}"));
            concreteSchema.Description = $"Tipo Marcacao: {legenda}.";
        }
    }
}
public class EnumTipoEmpresaSchemaFilter : ISchemaFilter
{
    private static readonly Dictionary<int, string> Sexo = new()
    {

        [1] = "Matriz",
        [2] = "Filial",
        [3] = "Unidade"

    };

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        if (context.Type == typeof(TipoEmpresa))
        {
            var legenda = string.Join(", ", Sexo.Select(x => $"{x.Key} = {x.Value}"));
            concreteSchema.Description = $"Tipo: {legenda}.";
        }
    }
}

