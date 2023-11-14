namespace OpenSkinsApi.Modules.Auth.Domain.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public static readonly string DefaultMessage = "Token is invalid";
        public InvalidTokenException(string? message) : base(message ?? DefaultMessage)
        {
        }

    }
}