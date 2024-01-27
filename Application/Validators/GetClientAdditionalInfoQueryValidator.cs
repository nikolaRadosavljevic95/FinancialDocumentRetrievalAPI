using Application.Queries;
using FluentValidation;

namespace Application.Validators;

public class GetClientAdditionalInfoQueryValidator : AbstractValidator<GetClientAdditionalInfoQuery>
{
    public GetClientAdditionalInfoQueryValidator()
    {
        RuleFor(x => x.ClientVAT)
            .NotEmpty()
            .WithMessage("Client VAT is required.");
    }
}
