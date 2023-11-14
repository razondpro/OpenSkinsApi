using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;
using FluentValidation;

namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequestDto>
    {

        public LoginUserValidator()
        {

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(Password.MaxLength)
                .MinimumLength(Password.MinLength)
                .Matches(Password.PasswordRegex);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);
        }
    }
}