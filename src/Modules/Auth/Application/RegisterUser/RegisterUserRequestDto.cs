namespace OpenSkinsApi.Modules.Auth.Application.RegisterUser
{
    public sealed record RegisterUserRequestDto(
        string FirstName,
        string? LastName,
        string Email,
        string UserName,
        string Password
    );
}