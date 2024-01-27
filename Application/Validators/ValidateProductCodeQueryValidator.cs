using Application.Queries;
using FluentValidation;

namespace Application.Validators;

public class ValidateProductCodeQueryValidator : AbstractValidator<ValidateProductCodeQuery>
{
    public ValidateProductCodeQueryValidator()
    {
        RuleFor(x => x.ProductCode)
            .NotEmpty()
            .WithMessage("Product code is required.");
    }
}
