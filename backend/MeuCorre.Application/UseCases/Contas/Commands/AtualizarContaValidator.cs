using FluentValidation;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class AtualizarContaValidator : AbstractValidator<AtualizarContaCommand>
    {
        public AtualizarContaValidator()
        {
            RuleFor(x => x.ContaId)
                .NotEmpty().WithMessage("ContaId é obrigatório.");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("UsuarioId é obrigatório.");

            RuleFor(x => x.Nome)
                .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.Nome))
                .WithMessage("Nome deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Cor)
                .Matches("^#([0-9A-Fa-f]{6})$").When(x => !string.IsNullOrWhiteSpace(x.Cor))
                .WithMessage("Cor inválida. Use o formato hexadecimal #RRGGBB.");

            RuleFor(x => x.DiaVencimento)
                .InclusiveBetween(1, 31).When(x => x.DiaVencimento.HasValue)
                .WithMessage("Dia de vencimento deve estar entre 1 e 31.");

            RuleFor(x => x.DiaFechamento)
                .InclusiveBetween(1, 31).When(x => x.DiaFechamento.HasValue)
                .WithMessage("Dia de fechamento deve estar entre 1 e 31.");
        }
    }
}
