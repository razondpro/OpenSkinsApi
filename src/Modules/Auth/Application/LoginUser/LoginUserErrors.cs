namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    using OpenSkinsApi.Application.Exceptions;

    public class PasswordMismatchError : ApplicationError
    {
        private const string DefaultMessage = "Password mismatch";
        public PasswordMismatchError() : base(DefaultMessage)
        {
        }
    }

    public class UserNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "User not found";
        public UserNotFoundError() : base(DefaultMessage)
        {
        }
    }

    public class UserNotVerifiedError : ApplicationError
    {
        private const string DefaultMessage = "User not verified";
        public UserNotVerifiedError() : base(DefaultMessage)
        {
        }
    }
}