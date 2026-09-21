using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class PeriodosConfiguration : IEntityTypeConfiguration<Periodo>
    {
        public void Configure(EntityTypeBuilder<Periodo> builder)
        {
            builder.Property(x => x.DataInicio)
            .HasColumnType("date")
            .IsRequired();

            builder.Property(x => x.DataFim)
                .HasColumnType("date")
                .HasDefaultValue(new DateOnly(2099, 12, 31))
                .IsRequired();

            builder.Property(x => x.Aberto)
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.DataUltAtualizacao)
                .HasColumnType("datetimeoffset(0)");

            
            builder.HasOne(x => x.Empresa)
                .WithMany(x=>x.Periodos)
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(x => new { x.EmpresaId, x.DataInicio })
                .IsUnique()
                .HasDatabaseName("IX_Periodos_Empresa_DataInicio");

            
            builder.HasIndex(x => new { x.EmpresaId, x.Aberto, x.DataInicio, x.DataFim })
                .HasDatabaseName("IX_Periodos_Empresa_Status_Datas");
        }
    }
}
