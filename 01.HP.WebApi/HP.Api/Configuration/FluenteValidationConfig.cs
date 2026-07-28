using FluentValidation;
using HP.Manager.Validator.Empresa;
using System.Globalization;
using System.Text.Json.Serialization;

namespace HP.Api.Configuration
{
    public static class FluenteValidationConfig
    {

        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            // 1. Configuração do JSON para Minimal APIs (Enum como String)
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // 2. Registra TODOS os validadores do Assembly de uma só vez
            // Passar apenas UM validador já descobre todos os outros do mesmo projeto/assembly!
            services.AddValidatorsFromAssemblyContaining<NovaEmpresaValidator>();

            // 3. Configura o idioma global do FluentValidation para Português
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            return services;
        }
    }
}

