using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class JornadaConfiguration : IEntityTypeConfiguration<Jornada>
    {
        public void Configure(EntityTypeBuilder<Jornada> builder)
        {

            builder.Property(j => j.DiaSemana)
                .IsRequired();
            
            builder.Property(j => j.Entrada1)
                .IsRequired();

            builder.Property(j => j.Saida1)
                .IsRequired();

            
            builder.Property(j => j.Entrada2)
                .IsRequired(false);

            builder.Property(j => j.Saida2)
                .IsRequired(false);

            
            builder.Property(j => j.Entrada3)
                .IsRequired(false);

            builder.Property(j => j.Saida3)
                .IsRequired(false);
        }
    }
}
