using MediatR;
using MeuCorre.Application.UseCases.Contas.Queries;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class SaldoConsolidadoQuery : IRequest<decimal>
    {
        public Guid UsuarioId { get; set; }
    }
}
