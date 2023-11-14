using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Auth.Application.RevokeToken
{
    public class RefreshTokenNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "Refresh token not found";
        public RefreshTokenNotFoundError() : base(DefaultMessage)
        {
        }
    }

    public class InvalidRefreshTokenError : ApplicationError
    {
        private const string DefaultMessage = "Invalid refresh token";
        public InvalidRefreshTokenError() : base(DefaultMessage)
        {
        }
    }
}