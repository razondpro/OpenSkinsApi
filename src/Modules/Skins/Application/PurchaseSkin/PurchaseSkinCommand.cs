using LanguageExt;
using OpenSkinsApi.Application.Commands;

namespace OpenSkinsApi.Modules.Skins.Application.PurchaseSkin
{
    public record PurchaseSkinCommand(string Email, string SkinId) : ICommand<Either<Exception, Unit>>;
}