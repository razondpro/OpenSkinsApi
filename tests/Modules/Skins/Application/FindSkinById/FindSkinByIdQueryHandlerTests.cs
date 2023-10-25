namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindSkinById
{
    using FluentAssertions;
    using Moq;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Application.FindSkinById;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Repositories;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;

    public class FindSkinByIdQueryHandlerTests
    {
        private readonly Mock<ISkinReadRepository> _skinReadRepositoryMock;

        public FindSkinByIdQueryHandlerTests()
        {
            _skinReadRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_Return_Skin_When_Repository_Returns_Skin()
        {
            // Arrange
            var query = new FindSkinByIdQuery(Guid.NewGuid().ToString());
            var handler = new FindSkinByIdQueryHandler(_skinReadRepositoryMock.Object);
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Rare, Color.Red);

            _skinReadRepositoryMock.Setup(x => x.Get(It.IsAny<UniqueIdentity>())).ReturnsAsync(skin);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsRight.Should().BeTrue();
            result.IfRight(x => x.Should().BeEquivalentTo(skin));
        }

        [Fact]
        public async void Handle_Should_Return_SkinNotFoundByIdError_When_Repository_Returns_Null()
        {
            // Arrange
            var query = new FindSkinByIdQuery(Guid.NewGuid().ToString());
            var handler = new FindSkinByIdQueryHandler(_skinReadRepositoryMock.Object);

            _skinReadRepositoryMock.Setup(x => x.Get(It.IsAny<UniqueIdentity>())).ReturnsAsync(null as Skin);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinNotFoundByIdError>());
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Repository_Throws_Exception()
        {
            // Arrange
            var query = new FindSkinByIdQuery(Guid.NewGuid().ToString());
            var handler = new FindSkinByIdQueryHandler(_skinReadRepositoryMock.Object);

            _skinReadRepositoryMock.Setup(x => x.Get(It.IsAny<UniqueIdentity>())).ThrowsAsync(new Exception("Test exception"));

            // Act
            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Test exception");
        }
    }
}