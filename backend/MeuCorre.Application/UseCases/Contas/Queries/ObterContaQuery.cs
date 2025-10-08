using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;
using Application.Interfaces;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ObterContaQuery : IRequest<ContaDetalheResponse>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class ContaDetalheResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Icone { get; set; }
        public string? Cor { get; set; }
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }

    public class ObterContaQueryHandler : IRequestHandler<ObterContaQuery, ContaDetalheResponse>
    {
        private readonly IContaRepository _contaRepository;

        public ObterContaQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ContaDetalheResponse> Handle(ObterContaQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);

            if (conta == null)
                throw new Exception("Conta não encontrada ou não pertence ao usuário.");

            return new ContaDetalheResponse
            {
                Id = conta.Id,
                Nome = conta.Nome,
                Icone = conta.Icone,
                Cor = conta.Cor,
                Saldo = conta.Saldo,
                Limite = conta.Limite,
                DiaVencimento = conta.DiaVencimento,
                DiaFechamento = conta.DiaFechamento,
                Ativo = conta.Ativo,
                DataCriacao = conta.DataCriacao,
                Tipo = conta.Tipo.ToString()
            };
        }
    }
}
