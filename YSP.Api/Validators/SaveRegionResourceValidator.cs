using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveRegionResourceValidator : AbstractValidator<SaveRegionResource>
    {
        public SaveRegionResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Name' must not be empty and less or equal 50.");
        }
    }
}
