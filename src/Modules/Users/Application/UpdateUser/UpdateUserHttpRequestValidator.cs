using FluentValidation;
using OpenSkinsApi.Modules.Users.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    public class UpdateUserHttpRequestValidator : AbstractValidator<UpdateUserHttpRequestDto>
    {
        public UpdateUserHttpRequestValidator()
        {
            When(x => !string.IsNullOrEmpty(x.FirstName), () =>
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .MinimumLength(Name.MinLength)
                    .MaximumLength(Name.MaxLength)
                    .Matches(Name.NameRegex)
            );

            When(x => !string.IsNullOrEmpty(x.LastName), () =>
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .MinimumLength(Name.MinLength)
                    .MaximumLength(Name.MaxLength)
                    .Matches(Name.NameRegex)
            );
        }

    }
}