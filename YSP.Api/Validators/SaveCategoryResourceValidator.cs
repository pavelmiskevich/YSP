using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveCategoryResourceValidator : AbstractValidator<SaveCategoryResource>
    {
        public SaveCategoryResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Name' must not be empty and less or equal 250.");
        }
    }
}
