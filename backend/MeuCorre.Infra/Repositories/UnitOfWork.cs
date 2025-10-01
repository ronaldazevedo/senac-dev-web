using MeuCorre.Application.Interfaces;
using MeuCorre.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeuDbContext _context;

        public UnitOfWork(MeuDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
