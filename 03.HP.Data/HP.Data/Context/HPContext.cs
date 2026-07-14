using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Context
{
    public class HPContext : DbContext
    {
        public HPContext(DbContextOptions options) : base(options)
        {
        }

        protected HPContext()
        {
        }
    }
}
