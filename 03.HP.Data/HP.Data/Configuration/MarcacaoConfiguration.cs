using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class MarcacaoConfiguration : IEntityTypeConfiguration<Marcacao>
    {
        public void Configure(EntityTypeBuilder<Marcacao> builder)
        {

            builder.Property(m => m.DataPersistencia)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();

            builder.Property(m => m.DataHora)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();

            builder.Property(m => m.RelogioId);

            builder.Property(m => m.TipoMarcacao)
                .IsRequired();

            builder.Property(m => m.NSR);

            builder.Property(m => m.CPF)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength();

            builder.Property(p => p.PIS)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(m => m.OrigemMarcacao)
                .IsRequired();

            builder.Property(m => m.Justificativa)
                .HasMaxLength(100);

            builder.HasIndex(m => new { m.RelogioId, m.NSR })
                   .IsUnique()
                   .HasFilter("[RelogioId] IS NOT NULL AND [NSR] IS NOT NULL");

            builder.HasIndex(m => new { m.CPF, m.DataHora })
                   .IsUnique();
          
            builder.HasIndex(m => m.DataHora);
            builder.HasIndex(m => m.NSR);
        }
    }

}
