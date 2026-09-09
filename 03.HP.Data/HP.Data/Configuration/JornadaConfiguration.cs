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

            builder.Property(j => j.MinutosCargaHoraria)
       .HasComputedColumnSql(
           "(CASE WHEN DATEDIFF(MINUTE, Entrada1, Saida1) < 0 " +
           "THEN DATEDIFF(MINUTE, Entrada1, Saida1) + 1440 " +
           "ELSE DATEDIFF(MINUTE, Entrada1, Saida1) END) " +
           "+ ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada2, Saida2) < 0 " +
           "THEN DATEDIFF(MINUTE, Entrada2, Saida2) + 1440 " +
           "ELSE DATEDIFF(MINUTE, Entrada2, Saida2) END, 0) " +
           "+ ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada3, Saida3) < 0 " +
           "THEN DATEDIFF(MINUTE, Entrada3, Saida3) + 1440 " +
           "ELSE DATEDIFF(MINUTE, Entrada3, Saida3) END, 0)", stored: true
        );
        }
    }
}
