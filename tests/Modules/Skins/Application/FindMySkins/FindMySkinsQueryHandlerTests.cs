namespace OpenSkinsApi.Tests.Modules.Skins.Application.FindMySkins
{
    using FluentAssertions;
    using Moq;
    using OpenSkinsApi.Modules.Skins.Application.FindMySkins;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Repositories;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    using Xunit;

    public class FindMySkinsQueryHandlerTests
    {
        private readonly Mock<IOwnerReadRepository> _ownerReadRepositoryMock;
        private readonly Mock<IPurchaseReadRepository> _purchaseReadRepositoryMock;

        public FindMySkinsQueryHandlerTests()
        {
            _ownerReadRepositoryMock = new();
            _purchaseReadRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_Return_AListOfPurchases_When_Owner_Is_Found()
        {
            // Arrange
            var cmd = new FindMySkinsQuery("test@example.com");
            var handler = new FindMySkinsQueryHandler(_ownerReadRepositoryMock.Object, _purchaseReadRepositoryMock.Object);
            var owner = Owner.Create(null, Email.Create("test@example.com"));
            var skin = Skin.Create(null, Name.Create("Skin 1"), Money.Create(10.0m), Type.Rare, Color.Red);
            var purchases = new List<Purchase> { Purchase.Create(owner, skin) };

            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create("test@example.com"))).ReturnsAsync(owner);
            _purchaseReadRepositoryMock.Setup(x => x.GetByOwner(owner.Id)).ReturnsAsync(purchases);

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.IsRight.Should().BeTrue();
            result.IfRight(p => p.Should().BeEquivalentTo(purchases));
        }

        [Fact]
        public async void Handle_Should_Return_AnOwnerNotFoundError_When_Owner_Is_Not_Found()
        {
            // Arrange
            var cmd = new FindMySkinsQuery("test@example.com");
            var handler = new FindMySkinsQueryHandler(_ownerReadRepositoryMock.Object, _purchaseReadRepositoryMock.Object);

            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create("test@example.com"))).ReturnsAsync(null as Owner);

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.IsLeft.Should().BeTrue();
            result.IfLeft(e => e.Should().BeOfType<OwnerNotFoundError>());
        }

        [Fact]
        public async void Handle_Should_Return_AnEmptyList_When_Owner_Has_No_Purchases()
        {
            // Arrange
            var cmd = new FindMySkinsQuery("test@example.com");
            var handler = new FindMySkinsQueryHandler(_ownerReadRepositoryMock.Object, _purchaseReadRepositoryMock.Object);
            var owner = Owner.Create(null, Email.Create("test@example.com"));

            _ownerReadRepositoryMock.Setup(x => x.FindByEmail(Email.Create("test@example.com"))).ReturnsAsync(owner);
            _purchaseReadRepositoryMock.Setup(x => x.GetByOwner(owner.Id)).ReturnsAsync(new List<Purchase>());

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.IsRight.Should().BeTrue();
            result.IfRight(p => p.Should().BeEmpty());
        }
    }
}