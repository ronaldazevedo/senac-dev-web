using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> ObterPorIdAsync(Guid id);
        Task<List<T>> ObterTodosAsync();
        Task<List<T>> BuscarAsync(Expression<Func<T, bool>> filtro);
        Task AdicionarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task RemoverAsync(Guid id);
        Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro);
    }
}
