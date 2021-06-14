using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Business.Models.Validations
{
    public class BetValidation : AbstractValidator<Bet>
    {
        public BetValidation()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotNull();

            RuleFor(c => c.MatchId)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.HomeScoreBoard)
                .LessThan(0).WithMessage("O placar não pode ser um valor negativo")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.AwayScoreBoard)
                .LessThan(0).WithMessage("O placar não pode ser um valor negativo")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
