using MediatR;
using MeuCorre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ContaDetalheResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public bool Ativa { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }

    // Query
    public class ObterContaQuery : IRequest<ContaDetalheResponse>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    // Handler
    public class ObterContaQueryHandler : IRequestHandler<ObterContaQuery, ContaDetalheResponse>
    {
        private readonly IContaRepository _contaRepository;

        public ObterContaQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ContaDetalheResponse> Handle(ObterContaQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new KeyNotFoundException("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new UnauthorizedAccessException("Conta não pertence ao usuário informado.");

            return new ContaDetalheResponse
            {
                Id = conta.Id,
                UsuarioId = conta.UsuarioId,
                Nome = conta.Nome,
                Saldo = conta.Saldo,
                Limite = conta.Limite,
                DiaVencimento = conta.DiaVencimento,
                DiaFechamento = conta.DiaFechamento,
                Cor = conta.Cor,
                Icone = conta.Icone,
                Ativa = conta.Ativa,
                CriadoEm = conta.CriadoEm,
                Tipo = conta.Tipo.ToString()
            };
        }
    }
}
