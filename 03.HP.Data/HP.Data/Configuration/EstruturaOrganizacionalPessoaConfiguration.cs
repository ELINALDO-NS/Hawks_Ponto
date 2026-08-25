using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HP.Data.Configuration
{
    public class EstruturaOrganizacionalPessoaConfiguration : IEntityTypeConfiguration<EstruturaOrganizacionalPessoa>
    {
        public void Configure(EntityTypeBuilder<EstruturaOrganizacionalPessoa> builder)
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
                .WithMany(p => p.EstruturasOrganizacionais)
                .HasForeignKey(cp => cp.PessoaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(cp => cp.EstruturaOrganizacional)
                .WithMany()
                .HasForeignKey(cp => cp.EstruturaOrganizacionalId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(cp => new { cp.PessoaId, cp.DataInicio, cp.DataFim })
                .HasDatabaseName("IX_EstruturaOrganizacional_Pessoa_Vigencia");

            builder.HasIndex(cp => cp.PessoaId)
                .IsUnique()
                .HasFilter("[DataFim] IS NULL")
                .HasDatabaseName("IX_EstruturaOrganizacional_Pessoa_EstruturaOrganizacionalAtualUnico");
        }
    }
}
