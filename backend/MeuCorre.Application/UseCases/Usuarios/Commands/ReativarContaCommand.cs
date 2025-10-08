using Application.Interfaces;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ReativarContaCommand : IRequest
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ReativarContaCommandHandler : IRequestHandler<ReativarContaCommand>
    {
        private readonly IContaRepository _contaRepository;

        public ReativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Unit> Handle(ReativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new Exception("Conta não pertence ao usuário.");

            conta.Ativo = true;
            conta.DataAtualizacao = DateTime.UtcNow;

            await _contaRepository.AtualizarAsync(conta);

            return Unit.Value;
        }

        Task IRequestHandler<ReativarContaCommand>.Handle(ReativarContaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
