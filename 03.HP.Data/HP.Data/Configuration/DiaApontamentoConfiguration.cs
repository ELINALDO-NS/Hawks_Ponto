using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class DiaApontamentoConfiguration : IEntityTypeConfiguration<DiaApontamento>
    {
        public void Configure(EntityTypeBuilder<DiaApontamento> builder)
        {
            builder.Property(e => e.DataUltAtualizacao)
                 .HasColumnType("datetimeoffset(0)")
                 .IsRequired(false);


            builder.HasOne(d => d.Pessoa)
                   .WithMany(p => p.Apontamentos)
                   .HasForeignKey(d => d.PessoaId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(d => d.MinutosTrabalhados)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosAtraso)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosFalta)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosExtra)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosCredito)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosDebito)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosAdicionalNoturno)
                   .HasDefaultValue(0);

            builder.Property(d => d.MinutosDSR)
                   .HasDefaultValue(0);

            builder.HasIndex(d => d.PessoaId);
            builder.HasIndex(d => d.DataApontamento);
        }
    }
}
