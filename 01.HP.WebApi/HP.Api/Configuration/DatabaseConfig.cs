using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Api.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HPContext>(option => option.UseSqlServer(configuration.GetConnectionString("HPConection")));

        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var contex = serviceScope.ServiceProvider.GetService<HPContext>();
            contex.Database.Migrate();
            
        }
    }
}
