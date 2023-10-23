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
            Currency currency = Currency.EUR;

            var money = Money.Create(amount, currency);

            money.Should().NotBeNull();
            money.Amount.Should().Be(amount);
            money.Currency.Should().Be(currency);
        }

        [Theory]
        [InlineData(-5.0, Currency.EUR)]
        public void Create_InvalidAmount_ThrowsInvalidMoneyAmountException(decimal invalidAmount, Currency currency)
        {
            var ex = Assert.Throws<InvalidMoneyAmountException>(() => Money.Create(invalidAmount, currency));
            ex.Message.Should().Be("Amount cannot be negative.");
        }

        [Fact]
        public void Create_ValidMoney_ReturnsMoneyInstanceWithCorrectCurrency()
        {
            decimal amount = 15.0M;
            Currency currency = Currency.USD;

            var money = Money.Create(amount, currency);

            money.Currency.Should().Be(Currency.USD);
        }
    }
}
