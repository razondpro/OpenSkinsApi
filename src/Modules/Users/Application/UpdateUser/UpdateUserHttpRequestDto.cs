namespace OpenSkinsApi.Modules.Users.Application.UpdateUser
{
    public sealed record UpdateUserHttpRequestDto(
        string? FirstName,
        string? LastName
    );
}