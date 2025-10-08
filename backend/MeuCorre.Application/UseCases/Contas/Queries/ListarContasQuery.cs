using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;
using Application.Interfaces;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ListarContasQuery : IRequest<List<ContaResumoResponse>>
    {
        public Guid UsuarioId { get; set; }
        public TipoConta? FiltrarPorTipo { get; set; }
        public bool ApenasAtivas { get; set; } = true;
        public string? OrdenarPor { get; set; }
        public TipoConta? Tipo { get; set; }
    }

    public class ContaResumoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoConta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        public decimal? LimiteDisponivel { get; set; }
        public bool Ativo { get; set; }
    }

    public class ListarContasQueryHandler : IRequestHandler<ListarContasQuery, List<ContaResumoResponse>>
    {
        private readonly IContaRepository _contaRepository;

        public ListarContasQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<List<ContaResumoResponse>> Handle(ListarContasQuery request, CancellationToken cancellationToken)
        {
           
            var contas = await _contaRepository.ObterPorUsuarioAsync(request.UsuarioId);

            
            if (request.FiltrarPorTipo.HasValue)
                contas = contas.Where(c => c.Tipo == request.FiltrarPorTipo.Value).ToList();

            if (request.ApenasAtivas)
                contas = contas.Where(c => c.Ativo).ToList();

           
            contas = request.OrdenarPor?.ToLower() switch
            {
                "nome" => contas.OrderBy(c => c.Nome).ToList(),
                "tipo" => contas.OrderBy(c => c.Tipo).ToList(),
                "saldo" => contas.OrderByDescending(c => c.Saldo).ToList(),
                _ => contas.OrderBy(c => c.Nome).ToList() // padrão
            };

            
            var resposta = contas.Select(c => new ContaResumoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Tipo = c.Tipo,
                Saldo = c.Saldo,
                Limite = c.Limite,
                LimiteDisponivel = c.Tipo == TipoConta.CartaoCredito && c.Limite.HasValue
                    ? c.Limite.Value - c.Saldo
                    : null,
                Ativo = c.Ativo
            }).ToList();

            return resposta;
        }
    }
}
