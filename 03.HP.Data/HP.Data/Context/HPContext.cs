using HP.Core.Entities;
using HP.Data.Configuration;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Context
{
    public class HPContext : DbContext
    {
        public HPContext(DbContextOptions<HPContext> options) : base(options){}
        protected HPContext(){}
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EstruturaOrganizacional> EstruturasOrganizacionais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
        }

    }
}
