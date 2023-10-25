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
        private readonly Mock<IUserReadRepository> _userReadRepositoryMock;
        private readonly BuySkinCommandHandler _handler;

        public BuySkinCommandHandlerTests()
        {
            _skinReadRepositoryMock = new Mock<ISkinReadRepository>();
            _skinWriteRepositoryMock = new Mock<ISkinWriteRepository>();
            _userReadRepositoryMock = new Mock<IUserReadRepository>();
            _handler = new BuySkinCommandHandler(
                _skinReadRepositoryMock.Object,
                _skinWriteRepositoryMock.Object,
                _userReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenUserNotFound_ReturnsUserNotFoundError()
        {
            // Arrange
            var email = "test@example.com";
            var request = new BuySkinCommand(email, Guid.NewGuid().ToString());
            _userReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(null as User);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<UserNotFoundError>());
        }

        [Fact]
        public async Task Handle_WhenSkinNotFound_ReturnsSkinNotFoundError()
        {
            // Arrange
            var email = "test@example.com";
            var skinId = Guid.NewGuid();
            var request = new BuySkinCommand(email, skinId.ToString());
            var user = User.Create(null, Email.Create(email));
            _userReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(user);
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
            var user = User.Create(null, Email.Create(email));
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red);
            _userReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(user);
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
            var user = User.Create(null, Email.Create(email));
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red);
            skin.MakeItAvailable();
            var request = new BuySkinCommand(email, skinId.ToString());
            _userReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create(email))).ReturnsAsync(user);
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