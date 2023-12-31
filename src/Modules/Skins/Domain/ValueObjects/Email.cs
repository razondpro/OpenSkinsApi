namespace OpenSkinsApi.Modules.Skins.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Exceptions;

    public sealed class Email : ValueObject
    {
        public static readonly Regex EmailRegex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);
        public static readonly int MaxLength = 100;
        public string Value { get; init; }
        private Email(string email)
        {
            Value = email;
        }

        public static explicit operator string(Email email) => email.Value;
        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidEmailException("Email is required");
            }

            if (value.Length > MaxLength)
            {
                throw new InvalidEmailException($"Email must be less than {MaxLength} characters long");
            }

            if (!EmailRegex.IsMatch(value))
            {
                throw new InvalidEmailException("Email is invalid");
            }

            return new Email(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}