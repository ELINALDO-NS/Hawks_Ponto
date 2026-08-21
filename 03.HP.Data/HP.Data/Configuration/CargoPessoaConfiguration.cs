using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HP.Data.Configuration
{
    public class CargoPessoaConfiguration : IEntityTypeConfiguration<CargoPessoa>
    {
        public void Configure(EntityTypeBuilder<CargoPessoa> builder)
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
                .WithMany(p => p.Cargos) 
                .HasForeignKey(cp => cp.PessoaId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(cp => cp.Cargo)
                .WithMany()
                .HasForeignKey(cp => cp.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(cp => new { cp.PessoaId, cp.DataInicio, cp.DataFim })
                .HasDatabaseName("IX_CargoPessoa_Pessoa_Vigencia");
            
            builder.HasIndex(cp => cp.PessoaId)
                .IsUnique()
                .HasFilter("[DataFim] IS NULL")
                .HasDatabaseName("IX_CargoPessoa_Pessoa_CargoAtualUnico");
        }
    }
}
