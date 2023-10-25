namespace OpenSkinsApi.Tests.Modules.Skins.Application.BuySkin
{
    using FluentAssertions;
    using LanguageExt;
    using Moq;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Application.BuySkin;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Repositories;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;
    public class BuySkinCommandHandlerTests
    {
        private readonly Mock<ISkinReadRepository> _skinReadRepositoryMock;
        private readonly Mock<ISkinWriteRepository> _skinWriteRepositoryMock;
        private readonly Mock<IOwnerReadRepository> _ownerReadRepositoryMock;
        private readonly BuySkinCommandHandler _handler;

        public BuySkinCommandHandlerTests()
        {
            _skinReadRepositoryMock = new Mock<ISkinReadRepository>();
            _skinWriteRepositoryMock = new Mock<ISkinWriteRepository>();
            _ownerReadRepositoryMock = new Mock<IOwnerReadRepository>();
            _handler = new BuySkinCommandHandler(
                _skinReadRepositoryMock.Object,
                _skinWriteRepositoryMock.Object,
                _ownerReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOwnerNotFound_ReturnsOwnerNotFoundError()
        {
            // Arrange
            var email = "test@example.com";
            var request = new BuySkinCommand(email, Guid.NewGuid().ToString());
            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(null as Owner);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<OwnerNotFoundError>());
        }

        [Fact]
        public async Task Handle_WhenSkinNotFound_ReturnsSkinNotFoundError()
        {
            // Arrange
            var email = "test@example.com";
            var skinId = Guid.NewGuid();
            var request = new BuySkinCommand(email, skinId.ToString());
            var owner = Owner.Create(null, Email.Create(email));
            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(owner);
            _skinReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(skinId))).ReturnsAsync(null as Skin);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinNotFoundError>());
        }

        [Fact]
        public async Task Handle_WhenSkinNotAvailable_ReturnsSkinNotAvailableError()
        {
            // Arrange
            var email = "test@example.com";
            var skinId = Guid.NewGuid();
            var request = new BuySkinCommand(email, skinId.ToString());
            var owner = Owner.Create(null, Email.Create(email));
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red);
            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(owner);
            _skinReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(skinId))).ReturnsAsync(skin);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinNotAvailableError>());
        }

        [Fact]
        public async Task Handle_WhenValidRequest_ReturnsUnit()
        {
            // Arrange
            var email = "test@example.com";
            var skinId = Guid.NewGuid();
            var owner = Owner.Create(null, Email.Create(email));
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red);
            skin.MakeItAvailable();
            var request = new BuySkinCommand(email, skinId.ToString());
            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(owner);
            _skinReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(skinId))).ReturnsAsync(skin);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsRight.Should().BeTrue();
            result.IfRight(x => x.Should().Be(Unit.Default));
            _skinWriteRepositoryMock.Verify(x => x.Update(skin), Times.Once);
        }

    }
}