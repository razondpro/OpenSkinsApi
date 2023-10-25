using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public class SkinNotOwnedError : ApplicationError
    {
        private const string DefaultMessage = "Skin not owned";
        public SkinNotOwnedError() : base(DefaultMessage)
        {
        }
    }

}