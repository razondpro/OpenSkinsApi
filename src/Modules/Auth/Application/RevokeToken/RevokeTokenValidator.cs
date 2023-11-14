using OpenSkinsApi.Modules.Auth.Application.Abstractions;
using FluentValidation;

namespace OpenSkinsApi.Modules.Auth.Application.RevokeToken
{
    public class RevokeTokenValidator : AbstractValidator<RevokeTokenRequestDto>
    {
        public RevokeTokenValidator(IJwtProvider jwtProvider)
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required")
                .Must(jwtProvider.ValidateToken)
                .WithMessage("Invalid refresh token");
        }

    }
}