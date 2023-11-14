namespace OpenSkinsApi.Modules.Auth.Domain.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public static readonly string DefaultMessage = "Invalid refresh token";
        public InvalidRefreshTokenException(string? message) : base(message ?? DefaultMessage)
        {
        }

    }
}