using HP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace HP.Data.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {

            builder.Property(p => p.EmpresaId)
                .IsRequired();

            builder.Property(p => p.Matricula)
                .IsRequired();

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.DataNascimento)
                .IsRequired(false);

            builder.Property(p => p.DataAdmissao)
                .IsRequired();

            builder.Property(p => p.DataDemissao);


            builder.Property(p => p.DataCadastro)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();


            builder.Property(p => p.DataUltAtualizacao)
                .HasColumnType("datetimeoffset(0)");

            builder
            .HasOne(p => p.Endereco)
            .WithMany()
            .HasForeignKey(p => p.EnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Rg)
                .HasMaxLength(20);

            builder.Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(p => p.Pis)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.Telefone)
                .HasMaxLength(20);

            builder.Property(p => p.TelefoneCelular)
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasMaxLength(150);

            builder.Property(p => p.ControlaPonto)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.DataControlaPonto)
                .IsRequired();

            builder.Property(p => p.DataNaoControlaPonto);

            builder.Property(p => p.Sexo)
                .IsRequired();

            builder.Property(p => p.BaseHoras)
                .IsRequired();


            builder.Property(p => p.EstruturaId)
                .IsRequired();

           


            builder.HasOne(p => p.Empresa)
                .WithMany()
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(p => p.Estrutura)
                .WithMany()
                .HasForeignKey(p => p.EstruturaId)
                .OnDelete(DeleteBehavior.Restrict);            

            builder.HasIndex(p => new { p.EmpresaId, p.Matricula })
                .IsUnique();


            builder.HasIndex(p => p.Cpf)
                .IsUnique()
                .HasFilter("[DataDemissao] IS NULL");


            builder.HasIndex(p => p.Pis)
                .IsUnique()
                .HasFilter("[DataDemissao] IS NULL");

            builder.HasIndex(p => p.Nome);
        }
    }
}
