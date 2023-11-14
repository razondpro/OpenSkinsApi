using OpenSkinsApi.Modules.Auth.Application.Abstractions;
using FluentValidation;

namespace OpenSkinsApi.Modules.Auth.Application.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenValidator(IJwtProvider jwtProvider)
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required")
                .Must(jwtProvider.ValidateToken)
                .WithMessage("Invalid refresh token");
        }

    }
}