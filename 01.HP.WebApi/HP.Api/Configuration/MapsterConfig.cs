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
                new PessoaMapping()
                
            );
            config.Compile();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
           
        }
    }
}
