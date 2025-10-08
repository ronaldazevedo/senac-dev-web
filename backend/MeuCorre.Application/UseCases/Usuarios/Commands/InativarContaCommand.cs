using Application.Interfaces;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class InativarContaCommand : IRequest
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class InativarContaCommandHandler : IRequestHandler<InativarContaCommand>
    {
        private readonly IContaRepository _contaRepository;

        public InativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Unit> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new Exception("Conta não pertence ao usuário.");

            if (conta.Saldo != 0)
                throw new Exception("A conta não pode ser inativada com saldo diferente de zero.");

            conta.Ativo = false;
            conta.DataAtualizacao = DateTime.UtcNow;

            await _contaRepository.AtualizarAsync(conta);

            return Unit.Value;
        }

        Task IRequestHandler<InativarContaCommand>.Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
