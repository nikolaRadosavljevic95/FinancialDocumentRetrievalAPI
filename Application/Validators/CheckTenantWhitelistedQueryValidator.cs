using Application.Queries;
using FluentValidation;

namespace Application.Validators;

public class CheckTenantWhitelistedQueryValidator : AbstractValidator<CheckTenantWhitelistedQuery>
{
    public CheckTenantWhitelistedQueryValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("Tenant ID is required.");
    }
}
