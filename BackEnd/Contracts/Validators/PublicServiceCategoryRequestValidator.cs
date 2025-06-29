using BackEnd.Contracts.PublicServiceCategory;
using FluentValidation;

namespace BackEnd.Contracts.Validators;

public class PublicServiceCategoryRequestValidator : AbstractValidator<PublicServiceCategoryRequest>
{
    public PublicServiceCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}
