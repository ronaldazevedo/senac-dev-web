using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ExcluirContaCommand : IRequest<(string mensagem, bool sucesso, int status)>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Confirmar { get; set; }
    }

    public class ExcluirContaCommandHandler : IRequestHandler<ExcluirContaCommand, (string mensagem, bool sucesso, int status)>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExcluirContaCommandHandler(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string mensagem, bool sucesso, int status)> Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            if (!request.Confirmar)
                return ("Confirmação obrigatória para excluir a conta.", false, 409);

            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                return ("Conta não encontrada.", false, 404);

            if (conta.UsuarioId != request.UsuarioId)
                return ("Conta não pertence ao usuário informado.", false, 409);

            if (conta.Saldo != 0)
                return ("Conta não pode ser excluída pois o saldo não está zerado.", false, 409);

            // Futuro: validar se há transações vinculadas

            await _contaRepository.ExcluirAsync(conta);
            await _unitOfWork.CommitAsync();

            return ("Conta excluída com sucesso.", true, 200);
        }
    }
}
