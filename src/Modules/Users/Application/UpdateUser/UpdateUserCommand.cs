using LanguageExt;
using OpenSkinsApi.Application.Commands;

namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    public sealed record UpdateUserCommand(
        string UserId,
        string? FirstName,
        string? LastName
    ) : ICommand<Either<Exception, Unit>>;
}