using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class AtualizarContaCommand : IRequest<(string mensagem, bool sucesso)>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }

        // Campos editáveis
        public string? Nome { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public bool? Ativa { get; set; }
    }

    public class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand, (string mensagem, bool sucesso)>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarContaCommandHandler(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string mensagem, bool sucesso)> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                return ("Conta não encontrada.", false);

            if (conta.UsuarioId != request.UsuarioId)
                return ("Conta não pertence ao usuário informado.", false);

            // Não permitir alteração de Tipo ou Saldo
            // Atualizar apenas campos permitidos
            if (!string.IsNullOrWhiteSpace(request.Nome))
                conta.AlterarNome(request.Nome);

            if (!string.IsNullOrWhiteSpace(request.Cor))
            {
                if (!Conta.CorEhValida(request.Cor))
                    return ("Cor inválida. Use o formato hexadecimal #RRGGBB.", false);
                conta.AlterarCor(request.Cor);
            }

            if (!string.IsNullOrWhiteSpace(request.Icone))
                conta.AlterarIcone(request.Icone);

            if (request.Limite.HasValue)
                conta.AlterarLimite(request.Limite.Value);

            if (request.DiaVencimento.HasValue)
                conta.AlterarDiaVencimento(request.DiaVencimento.Value);

            if (request.DiaFechamento.HasValue)
                conta.AlterarDiaFechamento(request.DiaFechamento.Value);

            if (request.Ativa.HasValue)
                conta.AlterarStatus(request.Ativa.Value);

            conta.AtualizarData(DateTime.UtcNow);

            await _contaRepository.AtualizarAsync(conta);
            await _unitOfWork.CommitAsync();

            return ("Conta atualizada com sucesso.", true);
        }
    }
}
