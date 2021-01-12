using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveQueryResourceValidator : AbstractValidator<SaveQueryResource>
    {
        public SaveQueryResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Name' must not be empty and less or equal 250.");

            RuleFor(c => c.SiteId)
                .NotEmpty()
                .WithMessage("'Site Id' must not be 0.");
        }
    }
}
