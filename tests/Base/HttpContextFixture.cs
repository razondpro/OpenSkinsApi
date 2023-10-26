using System.Security.Claims;
using Moq;

namespace OpenSkinsApi.Tests.Base
{
    public class HttpContextFixture
    {
        public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; }

        public HttpContextFixture()
        {
            HttpContextAccessorMock = new Mock<IHttpContextAccessor>();
            SetupHttpContextAccessor();
        }

        private void SetupHttpContextAccessor()
        {

            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "johndoe@example.com") })
            );

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            HttpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(httpContext);
        }
    }
}