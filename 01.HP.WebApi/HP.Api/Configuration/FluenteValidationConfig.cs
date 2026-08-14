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
            
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
          
            services.AddValidatorsFromAssemblyContaining<NovaEmpresaValidator>();
           
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            return services;
        }
    }
}

