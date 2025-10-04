using FluentValidation;
using MeuCorre.Domain.Enums;
using MeuCorre.Application.UseCases.Contas.Commands;

namespace Application.UseCases.Contas.Commands
{
    public class CriarContaValidator : AbstractValidator<CriarContaCommand>
    {
        public CriarContaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 50).WithMessage("O nome deve ter entre 2 e 50 caracteres.");

            RuleFor(x => x.Tipo)
                .IsInEnum().WithMessage("Tipo de conta inválido.");

            When(x => x.Tipo == TipoConta.CartaoCredito, () =>
            {
                RuleFor(x => x.Limite)
                    .NotNull().WithMessage("Limite é obrigatório para cartão.")
                    .GreaterThan(0).WithMessage("Limite deve ser maior que zero.");

                RuleFor(x => x.DiaVencimento)
                    .NotNull().WithMessage("Dia de vencimento é obrigatório para cartão.")
                    .InclusiveBetween(1, 31).WithMessage("Dia de vencimento deve estar entre 1 e 31.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Cor), () =>
            {
                RuleFor(x => x.Cor)
                    .Matches("^#([A-Fa-f0-9]{6})$")
                    .WithMessage("A cor deve estar no formato hexadecimal #RRGGBB.");
            });
        }
    }
}
