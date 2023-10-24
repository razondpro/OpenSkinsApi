using FluentAssertions;
using Moq;
using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindAvailableSkins
{
    public class FindAvailableSkinsQueryHandlerTests
    {
        private readonly Mock<ISkinReadRepository> _skinReadRepositoryMock;

        public FindAvailableSkinsQueryHandlerTests()
        {
            _skinReadRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_Return_AListOfSkins()
        {
            var cmd = new FindAvailableSkinsQuery();
            var handler = new FindAvailableSkinsQueryHandler(_skinReadRepositoryMock.Object);

            _skinReadRepositoryMock.Setup(x => x.GetAvailable()).ReturnsAsync(new List<Skin>());

            var result = await handler.Handle(cmd, default);

            result.Should().BeOfType<List<Skin>>();
        }
    }
}