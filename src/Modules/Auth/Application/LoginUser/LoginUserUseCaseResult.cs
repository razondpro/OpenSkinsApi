namespace OpenSkinsApi.Modules.Auth.Application.LoginUser
{
    public class LoginUserUseCaseResult
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
        public LoginUserUseCaseResult(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}