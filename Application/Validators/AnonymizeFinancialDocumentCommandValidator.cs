using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class AnonymizeFinancialDocumentCommandValidator : AbstractValidator<AnonymizeFinancialDocumentCommand>
{
    public AnonymizeFinancialDocumentCommandValidator()
    {   
        RuleFor(x => x.FinancialDocumentJson)
            .NotEmpty()
            .WithMessage("Financial document json is required.");

        RuleFor(x => x.ProductCode)
            .NotEmpty()
            .WithMessage("Product code is required.");
    }
}
