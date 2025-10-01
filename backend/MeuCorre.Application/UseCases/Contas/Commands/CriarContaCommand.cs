using MediatR;
using MeuCorre.Application.Interfaces;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class CriarContaCommand : IRequest<(string mensagem, bool sucesso)>
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]
        public string Nome { get; set; }

        public Guid UsuarioId { get; set; }

        public TipoConta Tipo { get; set; }

        public decimal Saldo { get; set; }

        public string? Cor { get; set; }

        public decimal? Limite { get; set; }

        public int? DiaVencimento { get; set; }

        public int? DiaFechamento { get; set; }
    }

    public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, (string mensagem, bool sucesso)>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarContaCommandHandler(IContaRepository contaRepository, IUnitOfWork unitOfWork)
        {
            _contaRepository = contaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string mensagem, bool sucesso)> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            // Nome único por usuário
            var nomeExiste = await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome);
            if (nomeExiste)
                return ("Já existe uma conta com esse nome para o usuário.", false);

            // Tipo obrigatório
            if (!Enum.IsDefined(typeof(TipoConta), request.Tipo))
                return ("Tipo de conta inválido.", false);

            // Validações específicas para Cartão
            if (request.Tipo == TipoConta.Cartao)
            {
                if (!request.Limite.HasValue || request.Limite <= 0)
                    return ("Limite é obrigatório para contas do tipo Cartão.", false);

                if (!request.DiaVencimento.HasValue || request.DiaVencimento < 1 || request.DiaVencimento > 31)
                    return ("Dia de vencimento deve estar entre 1 e 31.", false);
            }

            // Validação da cor
            if (!string.IsNullOrWhiteSpace(request.Cor) && !Conta.CorEhValida(request.Cor))
                return ("Cor inválida. Use o formato hexadecimal #RRGGBB.", false);

            var conta = new Conta(request.Nome, request.Tipo, request.Saldo, request.UsuarioId)
            {
                Id = Guid.NewGuid(),
                Cor = request.Cor,
                Limite = request.Limite,
                DiaVencimento = request.DiaVencimento,
                DiaFechamento = request.DiaFechamento,
                Ativa = true,
                CriadoEm = DateTime.UtcNow
            };

            await _contaRepository.AdicionarAsync(conta);
            await _unitOfWork.CommitAsync();

            return ("Conta criada com sucesso.", true);
        }
    }
}