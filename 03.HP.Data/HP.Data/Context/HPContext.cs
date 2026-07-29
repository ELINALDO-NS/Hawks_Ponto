using HP.Core;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Context
{
    public class HPContext : DbContext
    {
        public HPContext(DbContextOptions<HPContext> options) : base(options)
        {
        }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        protected HPContext()
        {
        }
    }
}
