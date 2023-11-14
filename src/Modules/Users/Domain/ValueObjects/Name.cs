namespace OpenSkinsApi.Modules.Users.Domain.ValueObjects
{
    using System.Text.RegularExpressions;
    using OpenSkinsApi.Modules.Users.Domain.Exceptions;
    using OpenSkinsApi.Domain;

    public sealed class Name : ValueObject
    {
        public static readonly int MaxLength = 50;
        public static readonly int MinLength = 2;
        public static readonly Regex NameRegex = new(@"^[\p{L}\d'\s-]+$", RegexOptions.Compiled);
        public string FirstName { get; private set; }
        public string? LastName { get; private set; }
        private Name(string firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Name Create(string firstName, string? lastName)
        {

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidNameException("First name is required");
            }

            Validate(firstName);

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                Validate(lastName);
            }

            return new(firstName, lastName);
        }

        private static void Validate(string namePart)
        {
            if (namePart.Length < MinLength || namePart.Length > MaxLength)
            {
                throw new InvalidNameException($"Name must be between {MinLength} and {MaxLength} characters long");
            }

            if (!NameRegex.IsMatch(namePart))
            {
                throw new InvalidNameException("Name must contain only letters, numbers, spaces, dashes and apostrophes");
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName ?? string.Empty;
        }
    }
}