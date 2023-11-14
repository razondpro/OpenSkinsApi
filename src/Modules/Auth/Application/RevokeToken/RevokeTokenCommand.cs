using OpenSkinsApi.Application.Commands;
using LanguageExt;

namespace OpenSkinsApi.Modules.Auth.Application.RevokeToken
{
    public record RevokeTokenCommand(string RefreshToken) : ICommand<Either<Exception, Unit>>;
}