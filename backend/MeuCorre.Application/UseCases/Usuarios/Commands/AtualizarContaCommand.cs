using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;
using Application.Interfaces;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class AtualizarContaCommand : IRequest
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }

        // Campos editáveis
        public string Nome { get; set; } = string.Empty;
        public string? Icone { get; set; }
        public string? Cor { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public bool Ativo { get; set; }
    }

    public class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand>
    {
        private readonly IContaRepository _contaRepository;

        public AtualizarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Unit> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new Exception("Conta não pertence ao usuário.");

            // Não permitir alterar Tipo nem Saldo
            conta.Nome = request.Nome;
            conta.Icone = request.Icone;
            conta.Cor = request.Cor;
            conta.Limite = request.Limite;
            conta.DiaVencimento = request.DiaVencimento;
            conta.DiaFechamento = request.DiaFechamento;
            conta.Ativo = request.Ativo;
            conta.DataAtualizacao = DateTime.UtcNow;

            await _contaRepository.AtualizarAsync(conta);

            return Unit.Value; // ✅ Isso aqui é obrigatório!
        }

        Task IRequestHandler<AtualizarContaCommand>.Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
