namespace OpenSkinsApi.Modules.Auth.Application.RegisterUser
{
    using LanguageExt;
    using OpenSkinsApi.Application.Commands;

    public sealed record RegisterUserCommand(
        string FirstName,
        string? LastName,
        string Email,
        string UserName,
        string Password
        ) : ICommand<Either<Exception, Unit>>;
}