namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindAvailableSkins
{
    using FluentAssertions;
    using Moq;
    using OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Repositories;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;
    public class FindAvailableSkinsQueryHandlerTests
    {
        private readonly Mock<ISkinReadRepository> _skinReadRepositoryMock;

        public FindAvailableSkinsQueryHandlerTests()
        {
            _skinReadRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_Return_AListOfSkins_When_Repository_Returns_NonEmpty_List()
        {
            // Arrange
            var cmd = new FindAvailableSkinsQuery();
            var handler = new FindAvailableSkinsQueryHandler(_skinReadRepositoryMock.Object);
            var skins = new List<Skin> {
                 Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Rare, Color.Red)
            };

            _skinReadRepositoryMock.Setup(x => x.GetAvailable()).ReturnsAsync(skins);

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(skins);
        }

        [Fact]
        public async void Handle_Should_Return_AnEmptyList_When_Repository_Returns_Empty_List()
        {
            // Arrange
            var cmd = new FindAvailableSkinsQuery();
            var handler = new FindAvailableSkinsQueryHandler(_skinReadRepositoryMock.Object);

            _skinReadRepositoryMock.Setup(x => x.GetAvailable()).ReturnsAsync(new List<Skin>());

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async void Handle_Should_Throw_Exception_When_Repository_Throws_Exception()
        {
            // Arrange
            var cmd = new FindAvailableSkinsQuery();
            var handler = new FindAvailableSkinsQueryHandler(_skinReadRepositoryMock.Object);

            _skinReadRepositoryMock.Setup(x => x.GetAvailable()).ThrowsAsync(new Exception("Test exception"));

            // Act
            Func<Task> act = async () => await handler.Handle(cmd, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Test exception");
        }
    }
}