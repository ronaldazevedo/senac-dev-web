using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ReativarContaCommand : IRequest<(string mensagem, bool sucesso)>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ReativarContaCommandHandler : IRequestHandler<ReativarContaCommand, (string mensagem, bool sucesso)>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReativarContaCommandHandler(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string mensagem, bool sucesso)> Handle(ReativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                return ("Conta não encontrada.", false);

            if (conta.UsuarioId != request.UsuarioId)
                return ("Conta não pertence ao usuário informado.", false);

            conta.Reativar();

            await _contaRepository.AtualizarAsync(conta);
            await _unitOfWork.CommitAsync();

            return ("Conta reativada com sucesso.", true);
        }

    }
}
