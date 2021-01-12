using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveScheduleResourceValidator : AbstractValidator<SaveScheduleResource>
    {
        public SaveScheduleResourceValidator()
        {
            RuleFor(c => c.QueryId)
                .NotEmpty()
                .WithMessage("'Query Id' must not be 0.");
        }
    }
}
