using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ContaResumoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoConta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        public decimal? LimiteDisponivel { get; set; }
        public bool Ativa { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
    }

    // Query
    public class ListarContasQuery : IRequest<List<ContaResumoResponse>>
    {
        public Guid UsuarioId { get; set; }
        public TipoConta? FiltrarPorTipo { get; set; }
        public bool ApenasAtivas { get; set; } = true;
        public string? OrdenarPor { get; set; } // "nome", "saldo", "tipo"
    }

    // Handler
    public class ListarContasQueryHandler : IRequestHandler<ListarContasQuery, List<ContaResumoResponse>>
    {
        private readonly IContaRepository _contaRepository;

        public ListarContasQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<List<ContaResumoResponse>> Handle(ListarContasQuery request, CancellationToken cancellationToken)
        {
            var contas = await _contaRepository.ObterPorUsuarioAsync(request.UsuarioId, request.ApenasAtivas);

            // Filtro por tipo
            if (request.FiltrarPorTipo.HasValue)
                contas = contas.Where(c => c.Tipo == request.FiltrarPorTipo.Value).ToList();

            // Ordenação
            contas = request.OrdenarPor?.ToLower() switch
            {
                "nome" => contas.OrderBy(c => c.Nome).ToList(),
                "saldo" => contas.OrderByDescending(c => c.Saldo).ToList(),
                "tipo" => contas.OrderBy(c => c.Tipo).ToList(),
                _ => contas
            };

            // Mapeamento para DTO
            var resultado = contas.Select(c => new ContaResumoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Tipo = c.Tipo,
                Saldo = c.Saldo,
                Limite = c.Limite,
                LimiteDisponivel = c.Tipo == TipoConta.CartaoCredito && c.Limite.HasValue
                    ? c.Limite.Value - c.Saldo
                    : null,
                Ativa = c.Ativa,
                Cor = c.Cor,
                Icone = c.Icone
            }).ToList();

            return resultado;
        }
    }
}
