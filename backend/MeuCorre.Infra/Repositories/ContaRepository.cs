using Application.Interfaces;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _context;

        public ContaRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {
            var query = _context.Contas.Where(c => c.UsuarioId == usuarioId);

            if (apenasAtivas)
                query = query.Where(c => c.Ativo);

            return await query.ToListAsync();
        }

        public async Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Tipo == tipo)
                .ToListAsync();
        }

        public async Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            return await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == contaId && c.UsuarioId == usuarioId);
        }

        public async Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null)
        {
            var query = _context.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Nome == nome);

            if (contaIdExcluir.HasValue)
                query = query.Where(c => c.Id != contaIdExcluir.Value);

            return await query.AnyAsync();
        }

        public async Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Ativo)
                .SumAsync(c => c.Saldo);
        }

        public async Task<List<Conta>> ListarPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.Contas
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Conta?> ObterPorIdAsync(Guid contaId)
        {
            return await _context.Contas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == contaId);
        }

        public async Task AdicionarAsync(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public Task RemoverAsync(Conta conta)
        {
            throw new NotImplementedException();
        }
    }
}
