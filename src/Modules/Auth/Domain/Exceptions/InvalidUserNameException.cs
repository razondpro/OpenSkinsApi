namespace OpenSkinsApi.Modules.Auth.Domain.Exceptions
{

    public class InvalidUserNameException : Exception
    {
        public static readonly string DefaultMessage = "User name is invalid";
        public InvalidUserNameException(string? message) : base(message ?? DefaultMessage)
        {
        }
    }
}