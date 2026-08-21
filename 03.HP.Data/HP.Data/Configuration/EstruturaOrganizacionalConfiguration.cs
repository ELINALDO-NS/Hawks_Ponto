using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class EstruturaOrganizacionalConfiguration : IEntityTypeConfiguration<EstruturaOrganizacional>
    {
        public void Configure(EntityTypeBuilder<EstruturaOrganizacional> builder)
        {
            builder.Property(e => e.Codigo)
            .IsRequired();
            builder.HasIndex(e => new { e.EmpresaId, e.Codigo })
            .IsUnique();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.DataCadastro)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();

            builder.Property(e => e.DataUltAtualizacao)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired(false);
            
            builder.HasOne(e => e.Empresa)
                .WithMany() 
                .HasForeignKey(e => e.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne<EstruturaOrganizacional>()
                .WithMany()
                .HasForeignKey(e => e.EstruturaPaiId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
