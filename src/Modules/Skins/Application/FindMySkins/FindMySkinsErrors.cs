using OpenSkinsApi.Application.Exceptions;

namespace OpenSkinsApi.Modules.Skins.Application.FindMySkins
{
    public class OwnerNotFoundError : ApplicationError
    {
        private const string DefaultMessage = "Owner not found";
        public OwnerNotFoundError() : base(DefaultMessage)
        {
        }
    }

}