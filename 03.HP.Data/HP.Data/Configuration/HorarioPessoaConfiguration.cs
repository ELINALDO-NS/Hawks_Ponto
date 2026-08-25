using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Configuration
{
    public class HorarioPessoaConfiguration : IEntityTypeConfiguration<HorarioPessoa>
    {
        public void Configure(EntityTypeBuilder<HorarioPessoa> builder)
        {
            builder.Property(cp => cp.DataInicio)
       .IsRequired()
       .HasColumnType("date");

            builder.Property(cp => cp.DataFim)
                .HasColumnType("date");

            builder.Property(cp => cp.DataCadastro)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();


            builder.HasOne(cp => cp.Pessoa)
                .WithMany(p => p.Horarios)
                .HasForeignKey(cp => cp.PessoaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(cp => cp.Horario)
                .WithMany()
                .HasForeignKey(cp => cp.HorarioId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(cp => new { cp.PessoaId, cp.DataInicio, cp.DataFim })
                .HasDatabaseName("IX_Horario_Pessoa_Vigencia");

            builder.HasIndex(cp => cp.PessoaId)
                .IsUnique()
                .HasFilter("[DataFim] IS NULL")
                .HasDatabaseName("IX_Horario_Pessoa_CargoAtualUnico");
        }
    }
}
