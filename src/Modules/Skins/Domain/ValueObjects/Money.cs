namespace OpenSkinsApi.Modules.Skins.Domain.ValueObjects
{
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.Exceptions;

    public class Money : ValueObject
    {
        public decimal Amount { get; }
        private Money(decimal amount)
        {
            Amount = amount;
        }

        public static Money Create(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidMoneyAmountException("Amount cannot be negative.");
            }

            return new Money(amount);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }
    }
}
