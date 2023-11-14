namespace OpenSkinsApi.Modules.Auth.Config.Authentication
{
    using Microsoft.Extensions.Options;

    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            options.SecretKey = _configuration["JWT_SECRET"] ?? "ThisIsASecretKey";
            options.Issuer = _configuration["JWT_ISSUER"] ?? "khubd";
            options.Audience = _configuration["JWT_AUDIENCE"] ?? "khubd";
            options.RefreshTokenExpirationInMinutes = int.Parse(_configuration["JWT_ACCESS_TOKEN_EXPIRATION"] ?? "10");
            options.TokenExpirationInMinutes = int.Parse(_configuration["JWT_REFRESH_TOKEN_EXPIRATION"] ?? "60");
        }
    }

}