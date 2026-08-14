using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HP.Data.Configuration
{
    public class HorarioConfiguration : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.Property(e => e.Codigo)
              .HasMaxLength(10).
              IsRequired();

            builder.HasIndex(e => new { e.EmpresaId, e.Codigo })
            .IsUnique();

            builder.Property(h => h.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.DataCadastro)
                .IsRequired();

            builder.Property(h => h.DataUltAtualizacao)
                .IsRequired(false);

            builder.HasOne(h => h.Empresa)
                .WithMany()
                .HasForeignKey(h => h.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(h => h.Jornadas)
                .WithOne(j => j.Horario)
                .HasForeignKey(j => j.HorarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
