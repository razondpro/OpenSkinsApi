using LanguageExt;
using OpenSkinsApi.Application.Commands;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public record BuySkinCommand(string Email, string SkinId) : ICommand<Either<Exception, Unit>>;
}