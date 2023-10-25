using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public class OwnerNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "Owner not found";
        public OwnerNotFoundError() : base(DefaultMessage)
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