using Application.Queries;
using FluentValidation;

namespace Application.Validators;

public class GetFinancialDocumentQueryValidator : AbstractValidator<GetFinancialDocumentQuery>
{
    public GetFinancialDocumentQueryValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required.");

        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage("Document ID is required.");

        RuleFor(x => x.ProductCode)
            .NotEmpty()
            .WithMessage("Product code is required.");
    }
}
