using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]

        public string Nome { get; set; }
        public TipoConta Tipo { get; private set; }
        public decimal Saldo { get; set; }
        public string? Cor { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        
        
       
        public string? Icone { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public TipoLimite? TipoLimite { get; set; }

        public Usuario Usuario { get; set; } = null;   
        public bool Ativa { get; set; }
        public DateTime CriadoEm { get; set; }



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
        public bool CorEhValida(bool cor)
        {
            if (string.IsNullOrWhiteSpace(Cor)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(Cor, "^#([0-9A-Fa-f]{6})$");
        }

        public static bool CorEhValida(string cor)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cor, "^#([0-9A-Fa-f]{6})$");
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarCor(string cor)
        {
            Cor = cor;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarIcone(string icone)
        {
            Icone = icone;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarLimite(decimal limite)
        {
            Limite = limite;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarDiaVencimento(int dia)
        {
            DiaVencimento = dia;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarDiaFechamento(int dia)
        {
            DiaFechamento = dia;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarStatus(bool ativa)
        {
            Ativa = ativa;
            DataAtualizacao = DateTime.Now;
        }

        public void AtualizarData(DateTime data)
        {
            DataAtualizacao = data;
        }
        public void Reativar()
        {
            Ativo = true;
            DataAtualizacao = DateTime.Now;
        }

    }
}
