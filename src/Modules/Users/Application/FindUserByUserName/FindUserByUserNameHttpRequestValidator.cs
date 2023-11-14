namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    using FluentValidation;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;

    public class FindUserByUserNameHttpRequestValidator : AbstractValidator<FindUserByUserNameHttpRequestDto>
    {
        public FindUserByUserNameHttpRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(UserName.MinLength)
                .MaximumLength(UserName.MaxLength)
                .Matches(UserName.UserNameRegex);
        }
    }

}