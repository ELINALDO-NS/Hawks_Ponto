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
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Jornada> Jornadas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<CargoPessoa> CargoPessoa { get; set; }
        public DbSet<EstruturaOrganizacionalPessoa> EstruturaOrganizacionalPessoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new EstruturaOrganizacionalConfiguration());
            modelBuilder.ApplyConfiguration(new CargoConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new JornadaConfiguration());
            modelBuilder.ApplyConfiguration(new HorarioConfiguration());
            modelBuilder.ApplyConfiguration(new CargoPessoaConfiguration());
            modelBuilder.ApplyConfiguration(new EstruturaOrganizacionalPessoaConfiguration());
        }

    }
}
