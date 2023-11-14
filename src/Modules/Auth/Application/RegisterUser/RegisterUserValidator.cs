namespace OpenSkinsApi.Modules.Auth.Application.RegisterUser
{
    using FluentValidation;
    using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(Name.MinLength)
                .MaximumLength(Name.MaxLength)
                .Matches(Name.NameRegex);

            RuleFor(x => x.LastName)
                .MinimumLength(Name.MinLength)
                .MaximumLength(Name.MaxLength)
                .Matches(Name.NameRegex);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(UserName.MinLength)
                .MaximumLength(UserName.MaxLength)
                .Matches(UserName.UserNameRegex);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(Password.MinLength)
                .MaximumLength(Password.MaxLength)
                .Matches(Password.PasswordRegex);
        }
    }
}
