using MediatR;

namespace MeuCorre.API.Controllers
{
    internal class CalcularSaldoTotalQuery : IRequest<object>
    {
        public Guid UsuarioId { get; set; }
    }
}