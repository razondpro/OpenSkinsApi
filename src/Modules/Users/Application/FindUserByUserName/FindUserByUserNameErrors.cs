using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    public class UserNotFoundByEmailError : ApplicationError
    {
        private const string DefaultMessage = "User not found";
        public UserNotFoundByEmailError() : base(DefaultMessage)
        {
        }
    }
}