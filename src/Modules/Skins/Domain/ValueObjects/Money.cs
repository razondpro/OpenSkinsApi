namespace OpenSkinsApi.Modules.Skins.Domain.ValueObjects
{
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Exceptions;

    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        private Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new InvalidMoneyAmountException("Amount cannot be negative.");
            }

            return new Money(amount, currency);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
