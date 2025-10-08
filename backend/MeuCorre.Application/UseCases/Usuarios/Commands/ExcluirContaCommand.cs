using Application.Interfaces;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ExcluirContaCommand : IRequest
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Confirmar { get; set; }
    }

    public class ExcluirContaCommandHandler : IRequestHandler<ExcluirContaCommand>
    {
        private readonly IContaRepository _contaRepository;

        public ExcluirContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Unit> Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            if (!request.Confirmar)
                throw new InvalidOperationException("Confirmação obrigatória para excluir a conta.");

            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new Exception("Conta não pertence ao usuário.");

            if (conta.Saldo != 0)
                throw new InvalidOperationException("A conta não pode ser excluída com saldo diferente de zero.");

            

            await _contaRepository.RemoverAsync(conta);

            return Unit.Value;
        }

        Task IRequestHandler<ExcluirContaCommand>.Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
