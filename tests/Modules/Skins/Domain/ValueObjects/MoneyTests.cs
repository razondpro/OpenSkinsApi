using FluentAssertions;
using OpenSkinsApi.Modules.Skins.Domain.Exceptions;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using OpenSkinsApi.Modules.Skins.Domain.Enums;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Domain.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void Create_ValidMoney_ReturnsMoneyInstance()
        {
            decimal amount = 10.0M;

            var money = Money.Create(amount);

            money.Should().NotBeNull();
            money.Amount.Should().Be(amount);
        }

        [Theory]
        [InlineData(-5.0)]
        public void Create_InvalidAmount_ThrowsInvalidMoneyAmountException(decimal invalidAmount)
        {
            var ex = Assert.Throws<InvalidMoneyAmountException>(() => Money.Create(invalidAmount));
            ex.Message.Should().Be("Amount cannot be negative.");
        }
    }
}
