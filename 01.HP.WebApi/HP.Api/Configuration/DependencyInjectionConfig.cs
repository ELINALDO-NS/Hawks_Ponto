using HP.Data.Repository;
using HP.Manager.Implementation;
using HP.Manager.Interfaces.Managers;
using HP.Core.Interfaces.Repository;

namespace HP.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
           
            services.AddScoped<IEmpresaManager, EmpresaManager>();
            
            
        }
    }
}
