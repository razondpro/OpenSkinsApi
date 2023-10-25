using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public class SkinNotFoundByIdError : ApplicationError
    {
        private const string DefaultMessage = "Skin not found";
        public SkinNotFoundByIdError() : base(DefaultMessage)
        {
        }

    }
}