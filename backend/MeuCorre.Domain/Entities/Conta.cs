using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public TipoConta Tipo { get; private set; }
        public decimal Saldo { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public decimal? Limite { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaVencimento { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public TipoLimite? TipoLimite { get; set; }

        public Usuario Usuario { get; set; } = null;   
        public bool Ativa { get; set; }

        public Conta(string nome, TipoConta tipo, decimal saldo, Guid usuarioId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Tipo = tipo;
            Saldo = saldo;
            UsuarioId = usuarioId;
            Ativo = true;
            DataCriacao = DateTime.Now;
        }

        public void AtualizarSaldo(decimal valor)
        {
            Saldo += valor;
            DataAtualizacao = DateTime.Now;
        }
        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.Now;
        }

        public void DefinirLimite(decimal? novoLimite)
        {
            Limite = novoLimite;
            DataAtualizacao = DateTime.Now; 
        }
        public void DefinirTipoLimite(TipoLimite? tipoLimite)
        {
            if (Tipo == TipoConta.CartaoCredito)
            {
                TipoLimite = tipoLimite;
                DataAtualizacao = DateTime.Now;
            }
        }


    }
}
