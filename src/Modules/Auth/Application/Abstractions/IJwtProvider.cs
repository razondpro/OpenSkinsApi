namespace OpenSkinsApi.Modules.Auth.Application.Abstractions
{
    using OpenSkinsApi.Modules.Auth.Domain.Entities;

    public interface IJwtProvider
    {
        string Generate(User user);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
    }
}
