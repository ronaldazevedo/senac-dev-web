using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class InativarContaCommand : IRequest<(string mensagem, bool sucesso)>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class InativarContaCommandHandler : IRequestHandler<InativarContaCommand, (string mensagem, bool sucesso)>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InativarContaCommandHandler(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string mensagem, bool sucesso)> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                return ("Conta não encontrada.", false);

            if (conta.UsuarioId != request.UsuarioId)
                return ("Conta não pertence ao usuário informado.", false);

            if (conta.Saldo != 0)
                return ("Conta não pode ser inativada pois o saldo não está zerado.", false);

            conta.Desativar(); // Marca Ativo = false e atualiza DataAtualizacao

            await _contaRepository.AtualizarAsync(conta);
            await _unitOfWork.CommitAsync();

            return ("Conta inativada com sucesso.", true);
        }
    }
}
