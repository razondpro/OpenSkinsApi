namespace OpenSkinsApi.Modules.Auth.Config.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = String.Empty;

        public string Audience { get; set; } = String.Empty;

        public string SecretKey { get; set; } = String.Empty;

        public int TokenExpirationInMinutes { get; set; } = 5;

        public int RefreshTokenExpirationInMinutes { get; set; } = 60 * 24 * 28;
    }
}