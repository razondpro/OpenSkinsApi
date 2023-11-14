using OpenSkinsApi.Modules.Auth.Domain.Repositories;
using OpenSkinsApi.Application.Commands;
using LanguageExt;
using OpenSkinsApi.Modules.Auth.Application.Abstractions;

namespace OpenSkinsApi.Modules.Auth.Application.RevokeToken
{
    public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand, Either<Exception, Unit>>
    {
        private readonly IAuthRefreshTokenReadRepository _refreshTokenReadRepository;
        private readonly IAuthUnitOfWork _unitOfWork;

        public RevokeTokenCommandHandler(
            IAuthRefreshTokenReadRepository refreshTokenReadRepository,
           IAuthUnitOfWork unitOfWork)
        {
            _refreshTokenReadRepository = refreshTokenReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Exception, Unit>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenReadRepository.Get(request.RefreshToken);

            if (refreshToken is null)
            {
                return new RefreshTokenNotFoundError();
            }

            if (refreshToken.IsRevoked)
            {
                return new InvalidRefreshTokenError();
            }

            refreshToken.MarkAsRevoked();

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Default;
        }
    }
}