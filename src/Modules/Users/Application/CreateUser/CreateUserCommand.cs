namespace OpenSkinsApi.Modules.Users.Application.CreateUser
{
    using LanguageExt;
    using OpenSkinsApi.Application.Commands;

    public sealed record CreateUserCommand(
        string UserId,
        string UserName,
        string FirstName,
        string? LastName
    ) : ICommand<Either<Exception, Unit>>;
}