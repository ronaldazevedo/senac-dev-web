using FluentValidation;
using MeuCorre.Application.UseCases.Contas.Commands;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class AtualizarContaValidator : AbstractValidator<AtualizarContaCommand>
    {
        public AtualizarContaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 50).WithMessage("O nome deve ter entre 2 e 50 caracteres.");

            When(x => !string.IsNullOrWhiteSpace(x.Cor), () =>
            {
                RuleFor(x => x.Cor)
                    .Matches("^#([A-Fa-f0-9]{6})$").WithMessage("A cor deve estar no formato hexadecimal (#RRGGBB).");
            });

            When(x => x.DiaVencimento.HasValue, () =>
            {
                RuleFor(x => x.DiaVencimento.Value)
                    .InclusiveBetween(1, 31).WithMessage("Dia de vencimento deve estar entre 1 e 31.");
            });

            When(x => x.DiaFechamento.HasValue, () =>
            {
                RuleFor(x => x.DiaFechamento.Value)
                    .InclusiveBetween(1, 31).WithMessage("Dia de fechamento deve estar entre 1 e 31.");
            });
        }
    }
}
