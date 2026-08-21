using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(e => e.Codigo)
                .IsRequired();

            builder.HasIndex(e => e.Codigo)
                .IsUnique();

            builder.Property(e => e.RazaoSocial)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.CnpjCpf)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false);

            builder.HasIndex(e => e.CnpjCpf)
                .IsUnique();

            builder.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Site)
                .HasMaxLength(200);

            builder.Property(e => e.Email)
                .HasMaxLength(150);

            builder.Property(e => e.TipoEmpresa)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.DataCadastro)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();

            builder.Property(e => e.DataUltAtualizacao)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired(false);

            builder.HasOne(e => e.Endereco)
                 .WithOne()
                 .HasForeignKey<Empresa>(e => e.EnderecoId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Portaria1510)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.Portaria671)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
