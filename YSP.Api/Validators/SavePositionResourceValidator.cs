using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SavePositionResourceValidator : AbstractValidator<SavePositionResource>
    {
        public SavePositionResourceValidator()
        {
            RuleFor(c => c.QueryId)
                .NotEmpty()
                .WithMessage("'Query Id' must not be 0.");
        }
    }
}
