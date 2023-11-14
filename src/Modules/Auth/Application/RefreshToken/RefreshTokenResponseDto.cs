using OpenSkinsApi.Infrastructure.Http.Core;

namespace OpenSkinsApi.Modules.Auth.Application.RefreshToken
{
    public class RefreshTokenResponseDto : ApiHttpResponse
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
        public RefreshTokenResponseDto(string title, int status, RefreshTokenUseCaseResult loginUserUseCaseResult) : base(title, status)
        {
            Token = loginUserUseCaseResult.Token;
            RefreshToken = loginUserUseCaseResult.RefreshToken;
        }
    }
}