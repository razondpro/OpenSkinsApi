using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    public class LoginUserResponseDto : ApiHttpResponse
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
        public LoginUserResponseDto(string title, int status, LoginUserUseCaseResult loginUserUseCaseResult) : base(title, status)
        {
            Token = loginUserUseCaseResult.Token;
            RefreshToken = loginUserUseCaseResult.RefreshToken;
        }
    }
}