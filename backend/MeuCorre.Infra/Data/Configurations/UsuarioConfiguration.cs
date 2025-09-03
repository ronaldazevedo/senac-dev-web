using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //define o nome da tabela no banco de dados
            builder.ToTable("Usuarios");

            //define chave primaria
            builder.HasKey(usuario => usuario.Id);

            //define as propriedades da entidade e suas configurações
            builder.Property(usuario => usuario.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(usuario => usuario.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(usuario => usuario.Senha)
                .IsRequired();

            builder.Property(usuario => usuario.DataNascimento)
                .IsRequired();

            builder.Property(usuario => usuario.Ativo)
                .IsRequired();

            builder.Property(usuario => usuario.DataCriacao)
                .IsRequired();

            builder.Property(usuario => usuario.DataAtualizacao)
                .IsRequired(false);

            //define que o campo Email deve ser único
            builder.HasIndex(usuario => usuario.Email)
                .IsUnique();

            
            //Cria um índice único no campo Email para evitar emails duplicados






        }
    }
}
