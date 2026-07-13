using Microsoft.OpenApi;

namespace HP.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
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


            });
        }
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }
}
