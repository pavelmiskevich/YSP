using FluentValidation;
using YSP.Api.Resources;

namespace YSP.Api.Validators
{
    public class SaveUserResourceValidator : AbstractValidator<SaveUserResource>
    {
        public SaveUserResourceValidator()
        {
            RuleFor(c => c.Name)
                //.NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Name' must not be empty and less or equal 250.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Email' must not be empty and less or equal 250.");

            RuleFor(c => c.Password)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("'Password' must not be empty and less or equal 250.");

            RuleFor(c => c.YandexLogin)
                //.NotEmpty()
                .MaximumLength(250)
                .WithMessage("'YandexKey' must be less or equal 250.");

            RuleFor(c => c.YandexKey)
                //.NotEmpty()
                .MaximumLength(250)
                .WithMessage("'YandexKey' must be less or equal 250.");

            RuleFor(c => c.Ip)
                //.NotEmpty()
                .MaximumLength(20)
                .WithMessage("'Ip' must be less or equal 20.");

            //RuleFor(c => c.Limit)
            //    .NotEmpty()
            //    .WithMessage("'Limit' must be less or equal 250.");

            //RuleFor(c => c.FreeLimit)
            //    .NotEmpty()
            //    .WithMessage("'FreeLimit' must be less or equal 20.");
        }
    }
}
