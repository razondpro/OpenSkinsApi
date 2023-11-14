using Microsoft.AspNetCore.Mvc;

namespace OpenSkinsApi.Modules.Users.Application.FindUserByUserName
{
    public sealed record FindUserByUserNameHttpRequestDto([FromRoute(Name = "userName")] string UserName);
}