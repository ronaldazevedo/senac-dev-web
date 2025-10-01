using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace MeuCorre.Infra.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _context;

        public ContaRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<Conta?> ObterPorIdAsync(Guid id)
        {
            return await _context.Contas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Conta>> ObterTodosAsync()
        {
            return await _context.Contas
                .Include(c => c.Usuario)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<List<Conta>> BuscarAsync(Expression<Func<Conta, bool>> filtro)
        {
            return await _context.Contas
                .Where(filtro)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Conta entidade)
        {
            await _context.Contas.AddAsync(entidade);
        }

        public async Task AtualizarAsync(Conta entidade)
        {
            _context.Contas.Update(entidade);
            await Task.CompletedTask;
        }

        public async Task RemoverAsync(Guid id)
        {
            var conta = await ObterPorIdAsync(id);
            if (conta != null)
            {
                _context.Contas.Remove(conta);
            }
        }

        public async Task<bool> ExisteAsync(Expression<Func<Conta, bool>> filtro)
        {
            return await _context.Contas.AnyAsync(filtro);
        }

        public async Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId && (!apenasAtivas || c.Ativa))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Tipo == tipo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            return await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == contaId && c.UsuarioId == usuarioId);
        }

        public async Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null)
        {
            return await _context.Contas.AnyAsync(c =>
                c.UsuarioId == usuarioId &&
                c.Nome == nome &&
                (!contaIdExcluir.HasValue || c.Id != contaIdExcluir.Value));
        }

        public async Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Ativa)
                .SumAsync(c => c.Saldo);
        }
    }
}

