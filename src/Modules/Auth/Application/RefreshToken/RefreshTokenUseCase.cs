using OpenSkinsApi.Modules.Auth.Application.Abstractions;
using OpenSkinsApi.Modules.Auth.Domain.Repositories;
using OpenSkinsApi.Application.Core;
using LanguageExt;
using Serilog;

namespace OpenSkinsApi.Modules.Auth.Application.RefreshToken
{
    public class RefreshTokenUseCase : IUseCase<RefreshTokenRequestDto, RefreshTokenUseCaseResult>
    {
        private readonly IAuthRefreshTokenReadRepository _refreshTokenReadRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IAuthUnitOfWork _unitOfWork;

        public RefreshTokenUseCase(
            IAuthRefreshTokenReadRepository refreshTokenReadRepository,
            IJwtProvider jwtProvider,
            IAuthUnitOfWork unitOfWork)
        {
            _refreshTokenReadRepository = refreshTokenReadRepository;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Exception, RefreshTokenUseCaseResult>> Execute(RefreshTokenRequestDto requestDto)
        {
            var refreshToken = await _refreshTokenReadRepository.Get(requestDto.RefreshToken);
            if (refreshToken is null)
            {
                return new RefreshTokenNotFoundError();
            }

            if (refreshToken.IsRevoked || refreshToken.IsUsed)
            {
                Log.Warning("Refresh token {RefreshToken} is revoked or used", refreshToken.Token);
                return new InvalidRefreshTokenError();
            }

            if (!refreshToken.IsActive())
            {
                return new InvalidRefreshTokenError();
            }

            refreshToken.MarkAsUsed();

            var jwtToken = _jwtProvider.Generate(refreshToken.User);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            refreshToken.User.AddRefreshToken(newRefreshToken);

            await _unitOfWork.CommitAsync();

            return new RefreshTokenUseCaseResult(jwtToken, newRefreshToken);
        }
    }
}