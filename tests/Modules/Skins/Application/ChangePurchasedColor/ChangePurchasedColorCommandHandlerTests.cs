namespace OpenSkinsApi.Tests.Modules.Skins.Application.ChangePurchasedColor
{
    using FluentAssertions;
    using LanguageExt;
    using Moq;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Repositories;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;
    public class ChangePurchasedColorCommandHandlerTests
    {
        private readonly Mock<IPurchaseReadRepository> _purchaseReadRepositoryMock;
        private readonly ChangePurchasedColorCommandHandler _handler;

        public ChangePurchasedColorCommandHandlerTests()
        {
            _purchaseReadRepositoryMock = new Mock<IPurchaseReadRepository>();
            _handler = new ChangePurchasedColorCommandHandler(_purchaseReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenPurchaseNotFound_ReturnsSkinNotOwnedError()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var email = "test@example.com";
            var colorNumber = 0;
            var request = new ChangePurchasedColorCommand(colorNumber, purchaseId.ToString(), email);
            _purchaseReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(purchaseId))).ReturnsAsync(null as Purchase);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinNotOwnedError>());
        }

        [Fact]
        public async Task Handle_WhenPurchaseOwnerEmailDoesNotMatch_ReturnsSkinNotOwnedError()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var email = "test@example.com";
            var colorNumber = 0;
            var request = new ChangePurchasedColorCommand(colorNumber, purchaseId.ToString(), email);
            var purchase = Purchase.Create(
                Owner.Create(null, Email.Create("other@example.com")),
                Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red)
            );
            _purchaseReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(purchaseId))).ReturnsAsync(purchase);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinNotOwnedError>());
        }

        [Fact]
        public async Task Handle_WhenPurchaseAlreadyHasSameColor_ReturnsSkinAlreadyHasSameColorError()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var email = "test@example.com";
            var colorNumber = 0;
            var request = new ChangePurchasedColorCommand(colorNumber, purchaseId.ToString(), email);
            var purchase = Purchase.Create(
                Owner.Create(null, Email.Create("test@example.com")),
                Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red)
            );
            _purchaseReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(purchaseId))).ReturnsAsync(purchase);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(x => x.Should().BeOfType<SkinAlreadyHasSameColorError>());
        }

        [Fact]
        public async Task Handle_WhenValidRequest_ReturnsUnit()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var email = "test@example.com";
            var colorNumber = 1;
            var request = new ChangePurchasedColorCommand(colorNumber, purchaseId.ToString(), email);
            var purchase = Purchase.Create(
                Owner.Create(null, Email.Create("test@example.com")),
                Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Epic, Color.Red)
            );
            _purchaseReadRepositoryMock.Setup(x => x.Get(new UniqueIdentity(purchaseId))).ReturnsAsync(purchase);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsRight.Should().BeTrue();
            result.IfRight(x => x.Should().Be(Unit.Default));
            purchase.Color.Should().Be((Color)colorNumber);
        }
    }
}