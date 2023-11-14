namespace OpenSkinsApi.Modules.Auth.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public static readonly string DefaultMessage = "Password is invalid";
        public InvalidPasswordException(string? message) : base(message ?? DefaultMessage)
        {
        }

    }
}