using BackEnd.Contracts.PublicService;
using FluentValidation;

namespace BackEnd.Contracts.Validators;

public class PublicServiceRequestValidator : AbstractValidator<PublicServiceRequest>
{
    public PublicServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category Id must be valid.");
    }
}
