using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeuCorre.Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Cor)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(c => c.Icone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Saldo)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(c => c.UsuarioId)
                .IsRequired();

            builder.HasIndex(c => c.UsuarioId);
            builder.HasIndex(c => c.Tipo);
            builder.HasIndex(c => c.Ativo);
        }
    }
}
