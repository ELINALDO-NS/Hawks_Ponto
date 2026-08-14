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
                    options.SchemaFilter<TimeOnlySchemaFilter>();
                    options.SchemaFilter<DayOfWeekSchemaFilter>();

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