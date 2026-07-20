using HP.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Context
{
    public class HPContext : DbContext
    {
        public HPContext(DbContextOptions<HPContext> options) : base(options)
        {
        }
        public DbSet<Empresa> Empresa { get; set; }
        protected HPContext()
        {
        }
    }
}
