using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly MeuDbContext _meuDbcontext;

        public CategoriaRepository(MeuDbContext meuDbcontext)
        {
            _meuDbcontext = meuDbcontext;
        }

        public async Task<Categoria?> ObterPorIdAsync(Guid categoriaId)
        {
            var categoria = await _meuDbcontext.Categorias.FindAsync(categoriaId);
            return categoria;
        }

        public async Task<IEnumerable<Categoria>> ListarTodasPorUsuarioAsync(Guid usuarioId)
        {
            var listaCategorias = _meuDbcontext.Categorias.Where(c => c.UsuarioId == usuarioId);

            return await listaCategorias.ToListAsync();
        }

        public async Task<bool> ExisteAsync(Guid categoriaId)
        {
            var  existe = await _meuDbcontext.Categorias.AnyAsync(c => c.Id == categoriaId);

            return existe;
        }
    
        public  async Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId)
        {
            var existe = await _meuDbcontext.Categorias.AnyAsync(c => c.Nome == nome && c.UsuarioId == usuarioId && c.TipoDaTransacao == tipo);

            return existe;
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            _meuDbcontext.Categorias.Add(categoria);
            await _meuDbcontext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            _meuDbcontext.Categorias.Update(categoria);
            await _meuDbcontext.SaveChangesAsync();
        }

        public async Task RemoverAsync(Categoria categoria)
        {
            _meuDbcontext.Categorias.Remove(categoria);
            await _meuDbcontext.SaveChangesAsync();

        }
    }
}
