using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    public class UserNotFoundError : ApplicationError
    {
        public const string DefaultMessage = "User not found";
        public UserNotFoundError() : base(DefaultMessage)
        {
        }
    }
}