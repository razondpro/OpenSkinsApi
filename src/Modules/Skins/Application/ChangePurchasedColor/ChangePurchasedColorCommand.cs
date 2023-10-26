using LanguageExt;
using OpenSkinsApi.Application.Commands;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public record ChangePurchasedColorCommand(
        int ColorNumber,
        string PurchaseId,
        string OwnerEmail
    ) : ICommand<Either<Exception, Unit>>;
}