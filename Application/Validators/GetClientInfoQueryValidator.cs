using Application.Queries;
using FluentValidation;

namespace Application.Validators;

public class GetClientInfoQueryValidator : AbstractValidator<GetClientInfoQuery>
{
    public GetClientInfoQueryValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required.");

        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage("Document ID is required.");
    }
}