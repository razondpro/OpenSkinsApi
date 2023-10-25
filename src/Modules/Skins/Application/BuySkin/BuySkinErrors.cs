using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public class UserNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "User not found";
        public UserNotFoundError() : base(DefaultMessage)
        {
        }
    }
    public class SkinNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "Skin not found";
        public SkinNotFoundError() : base(DefaultMessage)
        {
        }
    }

    public class SkinNotAvailableError : ApplicationError
    {
        private const string DefaultMessage = "Skin is not available";
        public SkinNotAvailableError() : base(DefaultMessage)
        {
        }
    }

}