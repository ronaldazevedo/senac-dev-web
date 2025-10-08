using MeuCorre.Domain.Enums;
using System;
using System.Text.RegularExpressions;

namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {
        private string tipo;

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public TipoConta Tipo { get; set; } 
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

        public virtual Usuario Usuario { get; set; }

        public Conta(Guid id, string nome, TipoConta tipo, decimal saldo, Guid usuarioId) 
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            Saldo = saldo;
            UsuarioId = usuarioId;
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
        }

        public Conta(Guid id, string nome, string tipo, decimal saldo, Guid usuarioId) : base(id)
        {
            Nome = nome;
            this.tipo = tipo;
            Saldo = saldo;
            UsuarioId = usuarioId;
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
            AtualizarData();
        }

        public void AtualizarTipo(TipoConta tipo) 
        {
            Tipo = tipo;
            AtualizarData();
        }

        public void AtualizarSaldo(decimal novoSaldo)
        {
            Saldo = novoSaldo;
            AtualizarData();
        }

        public void AtualizarLimite(decimal? limite)
        {
            Limite = limite;
            AtualizarData();
        }

        public void AtualizarFechamentoEVencimento(int? fechamento, int? vencimento)
        {
            DiaFechamento = fechamento;
            DiaVencimento = vencimento;
            AtualizarData();
        }

        public void AtualizarVisual(string? cor, string? icone)
        {
            Cor = cor;
            Icone = icone;
            AtualizarData();
        }

        public void Desativar()
        {
            Ativo = false;
            AtualizarData();
        }

        public void DefinirTipoLimite(TipoLimite? tipoLimite)
        {
            TipoLimite = tipoLimite;
            AtualizarData();
        }

        private void AtualizarData()
        {
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("Nome da conta é obrigatório.");

            if (Nome.Length < 2 || Nome.Length > 50)
                throw new ArgumentException("Nome deve ter entre 2 e 50 caracteres.");

            if (Tipo == TipoConta.CartaoCredito) 
            {
                if (!Limite.HasValue || Limite <= 0)
                    throw new ArgumentException("Limite é obrigatório e deve ser maior que zero para cartão.");

                if (!DiaVencimento.HasValue || DiaVencimento < 1 || DiaVencimento > 31)
                    throw new ArgumentException("Dia de vencimento deve estar entre 1 e 31.");
            }

            if (!string.IsNullOrWhiteSpace(Cor) && !Regex.IsMatch(Cor, "^#([A-Fa-f0-9]{6})$"))
                throw new ArgumentException("Cor inválida. Use o formato #RRGGBB.");
        }
    }
}
