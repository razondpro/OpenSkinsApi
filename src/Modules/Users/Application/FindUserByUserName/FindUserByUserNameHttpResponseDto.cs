namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Infrastructure.Http.Core;

    public class FindUserByUserNameHttpResponseDto : ApiHttpResponse
    {
        public UserDto User { get; init; }

        public FindUserByUserNameHttpResponseDto(User user) : base("Ok", StatusCodes.Status200OK)
        {
            User = new UserDto(
                user.Id.Value.ToString(),
                user.UserName.Value,
                user.Name.FirstName,
                user.Name.LastName ?? string.Empty
            );
        }
    }

    public record UserDto(
        string Id,
        string UserName,
        string FirstName,
        string LastName
    );
}