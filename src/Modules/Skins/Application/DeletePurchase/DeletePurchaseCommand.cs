using LanguageExt;
using OpenSkinsApi.Application.Commands;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public record DeleteOwnedSkinCommand(string PurchaseId, string OwnerEmail) :
        ICommand<Either<Exception, Unit>>;
}