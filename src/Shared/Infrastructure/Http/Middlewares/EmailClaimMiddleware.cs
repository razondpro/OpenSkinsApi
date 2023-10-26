using System.Security.Claims;

namespace OpenSkinsApi.Infrastructure.Http.Middlewares
{
    public class EmailClaimMiddleware
    {
        private readonly RequestDelegate _next;

        public EmailClaimMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var email = "johndoe@example.com";
            var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) });
            context.User = new ClaimsPrincipal(claimsIdentity);

            await _next(context);
        }
    }
}