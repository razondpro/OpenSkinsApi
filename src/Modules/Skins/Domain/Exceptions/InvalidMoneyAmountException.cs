namespace OpenSkinsApi.Modules.Skins.Domain.Exceptions
{
    public class InvalidMoneyAmountException : Exception
    {
        public static readonly string DefaultMessage = "Money amount is invalid";

        public InvalidMoneyAmountException(string? message) : base(message ?? DefaultMessage)
        {
        }

    }
}