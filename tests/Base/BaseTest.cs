using Moq;
using Xunit;

namespace OpenSkinsApi.Tests.Base
{
    public class BaseTest : IClassFixture<HttpContextFixture>
    {
        protected readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public BaseTest(HttpContextFixture fixture)
        {
            _httpContextAccessorMock = fixture.HttpContextAccessorMock;
        }
    }
}