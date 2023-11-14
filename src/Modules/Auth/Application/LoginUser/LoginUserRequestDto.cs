namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    public record LoginUserRequestDto(
        string Email,
        string Password
    );
}