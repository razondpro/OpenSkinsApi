namespace OpenSkinsApi.Modules.Skins.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Exceptions;

    public sealed class Name : ValueObject
    {
        public static readonly int MaxLength = 100;
        public static readonly int MinLength = 2;
        public static readonly Regex NameRegex = new(@"^[\p{L}\d'\s-]+$", RegexOptions.Compiled);
        public string Value { get; init; }
        private Name(string value)
        {
            Value = value;
        }

        public static explicit operator string(Name name) => name.Value;

        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidNameException("Name is required");
            }

            if (value.Length < MinLength || value.Length > MaxLength)
            {
                throw new InvalidNameException($"Name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!NameRegex.IsMatch(value))
            {
                throw new InvalidNameException("Name must contain only letters and numbers");
            }

            return new Name(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}