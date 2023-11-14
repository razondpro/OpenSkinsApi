namespace OpenSkinsApi.Modules.Auth.Application.RefreshToken
{
    public class RefreshTokenUseCaseResult
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
        public RefreshTokenUseCaseResult(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}