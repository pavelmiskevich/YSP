using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveSiteResourceValidator : AbstractValidator<SaveSiteResource>
    {
        public SaveSiteResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Name' must not be empty and less or equal 250.");

            RuleFor(c => c.Url)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Url' must not be empty and less or equal 250.");

            RuleFor(c => c.Descr)
                //.NotEmpty()
                .MaximumLength(1000)
                .WithMessage("'Descr' must not be empty and less or equal 1000.");

            RuleFor(c => c.RegionId)
                .NotEmpty()
                .WithMessage("'Region Id' must not be 0.");
        }
    }
}
