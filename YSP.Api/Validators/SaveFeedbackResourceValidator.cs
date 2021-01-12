using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveFeedbackResourceValidator : AbstractValidator<SaveFeedbackResource>
    {
        public SaveFeedbackResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("'Name' must not be empty and less or equal 1000.");
        }
    }
}
