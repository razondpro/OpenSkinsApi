namespace OpenSkinsApi.Modules.Users.Application.CreateUser
{
    using OpenSkinsApi.Application.Exceptions;

    public class UserNameAlreadyExistsError : ApplicationError
    {
        private const string DefaultMessage = "Username already exists";
        public UserNameAlreadyExistsError() : base(DefaultMessage)
        {
        }
    }
}