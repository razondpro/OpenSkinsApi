using FluentValidation;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public class FindSkinByIdValidator : AbstractValidator<FindSkinByIdRequestDto>
    {
        public FindSkinByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("The Id must be a valid GUID"); ;
        }
    }
}