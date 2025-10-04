using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;
using Application.Interfaces;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class CriarContaCommand : IRequest<CriarContaResponse>
    {
        public string Nome { get; set; } = string.Empty;
        public TipoConta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
    }

    public class CriarContaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoConta Tipo { get; set; }
        public decimal Saldo { get; set; }
    }

    public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public CriarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            // Validação: nome único por usuário
            var contasDoUsuario = await _contaRepository.ObterPorUsuarioAsync(request.UsuarioId);
            if (contasDoUsuario.Any(c => c.Nome.ToLower() == request.Nome.ToLower()))
                throw new Exception("Já existe uma conta com esse nome para o usuário.");

            // Ajuste de saldo se for devedor
            var saldoFinal = request.Saldo < 0 ? request.Saldo * -1 : request.Saldo;

            // Cálculo do DiaFechamento se for cartão
            int? diaFechamento = request.DiaFechamento;
            if (request.Tipo == TipoConta.CartaoCredito && !request.DiaFechamento.HasValue && request.DiaVencimento.HasValue)
            {
                diaFechamento = request.DiaVencimento.Value - 10;
                if (diaFechamento < 1) diaFechamento = 1;
            }

            // Ajuste para o construtor atualizado de Conta
            var conta = new Conta(
                id: Guid.NewGuid(),
                nome: request.Nome,
                tipo: request.Tipo.ToString(),
                saldo: saldoFinal,
                usuarioId: request.UsuarioId
            )
            {
                Limite = request.Limite,
                DiaVencimento = request.DiaVencimento,
                DiaFechamento = diaFechamento,
                Cor = request.Cor,
                Icone = request.Icone,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            await _contaRepository.AdicionarAsync(conta);

            return new CriarContaResponse
            {
                Id = conta.Id,
                Nome = conta.Nome,
                Tipo = request.Tipo,
                Saldo = conta.Saldo
            };
        }
    }
}
