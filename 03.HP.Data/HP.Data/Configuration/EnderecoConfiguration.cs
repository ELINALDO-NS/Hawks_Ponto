using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HP.Data.Configuration
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength(false);


            builder.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(e => e.Complemento)
                .IsRequired(false)
                .HasMaxLength(150);


            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength();
        }
    }
}

