using FluentValidation;
using HP.Manager.Validator.Empresa;
using System.Globalization;
using System.Text.Json;
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
                options.SerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
                options.SerializerOptions.Converters.Add(new NullableDateTimeOffsetJsonConverter());
            });
          
            services.AddValidatorsFromAssemblyContaining<NovaEmpresaValidator>();
           
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            return services;
        }
    }
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        private const string Format = "yyyy-MM-ddTHH:mm:sszzz";

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTimeOffset.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(Format));
    }
    public class NullableDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset?>
    {
        private const string Format = "yyyy-MM-ddTHH:mm:sszzz";

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Null ? null : DateTimeOffset.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(Format));
            else
                writer.WriteNullValue();
        }
    }
}

