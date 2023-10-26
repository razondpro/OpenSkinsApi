using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public class SkinNotOwnedError : ApplicationError
    {
        private const string DefaultMessage = "Skin not owned";
        public SkinNotOwnedError() : base(DefaultMessage)
        {
        }
    }

    public class SkinAlreadyHasSameColorError : ApplicationError
    {
        private const string DefaultMessage = "Skin has already the same color";
        public SkinAlreadyHasSameColorError() : base(DefaultMessage)
        {
        }
    }

}