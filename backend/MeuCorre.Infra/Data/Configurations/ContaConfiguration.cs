using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Data.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            // Nome da tabela
            builder.ToTable("Contas");

            // Chave primária
            builder.HasKey(c => c.Id);

            // Campos obrigatórios
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.Saldo)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.UsuarioId)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .IsRequired();

           
            builder.Property(c => c.Limite)
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.DiaFechamento);

            builder.Property(c => c.DiaVencimento);

            builder.Property(c => c.Cor)
                .HasMaxLength(7);

            builder.Property(c => c.Icone)
                .HasMaxLength(20);

            builder.Property(c => c.DataAtualizacao);

            builder.Property(c => c.TipoLimite);

            // Relacionamento com Usuario
            builder.HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(c => c.UsuarioId);
            builder.HasIndex(c => c.Tipo);
            builder.HasIndex(c => c.Ativo);
        }
    }
}
