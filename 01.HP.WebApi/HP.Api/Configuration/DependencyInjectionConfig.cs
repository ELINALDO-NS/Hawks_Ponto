using HP.Data.Repository;
using HP.Manager.Implementation;
using HP.Core.Interfaces.Repository;
using HP.Core.Interfaces;
using HP.Manager.Interfaces;

namespace HP.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IEstruturaOrganizacionalRepository, EstruturaOrganizacionalRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            services.AddScoped<IMarcacaoRepository, MarcacaoRepository>();


            services.AddScoped<IEmpresaManager, EmpresaManager>();
            services.AddScoped<IEstruturaOrganizacionalManager, EstruturaOrganizacionalManager>();
            services.AddScoped<ICargoManager, CargoManager>();
            services.AddScoped<IPessoaManager, PessoaManager>();
            services.AddScoped<IHorarioManager, HorarioManager>();
            services.AddScoped<IMarcacaoManager, MarcacaoManager>();

            
            
        }
    }
}
