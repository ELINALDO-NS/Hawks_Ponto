using HP.Manager.Mappings;
using Mapster;
using MapsterMapper;

namespace HP.Api.Configuration
{
    public static class MapsterConfig
    {
        public static void AddMapsterConfiguration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.RequireExplicitMapping = true;
            config.Apply(
                new EmpresaMapping(),
                new EnderecoMapping(),
                new EstruturaOrganizacionalMapping(),
                new CargoMapping(),
                new PessoaMapping(),
                new JornadaMapping(),
                new HorarioMapping(),
                new MarcacaoMapping()

            );
            try
            {
                config.Compile();
            }
            catch (CompileException ex)
            {
                var mensagemErro = ex.Message;
                var erroInterno = ex.InnerException?.Message;
                throw new InvalidOperationException(
                $"Falha no mapeamento do Mapster.\nDetalhes: {mensagemErro}\nCausa: {erroInterno}", ex);
            }
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

        }
    }
}
