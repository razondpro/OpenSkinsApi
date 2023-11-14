using System.Security.Cryptography;
using System.Text.RegularExpressions;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Auth.Domain.Exceptions;

namespace OpenSkinsApi.Modules.Auth.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public static readonly int MinLength = 8;
        public static readonly int MaxLength = 100;
        public static readonly Regex PasswordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,100}$", RegexOptions.Compiled);
        public byte[] Hash { get; }
        public byte[] Salt { get; }
        private Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public static Password Create(string plainTextPassword)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword))
            {
                throw new InvalidPasswordException("Password is required");
            }

            if (plainTextPassword.Length < MinLength || plainTextPassword.Length > MaxLength)
            {
                throw new InvalidPasswordException($"Password must be between {MinLength} and {MaxLength} characters long");
            }

            if (!PasswordRegex.IsMatch(plainTextPassword))
            {
                throw new InvalidPasswordException("Password is invalid");
            }

            var salt = GenerateSalt();
            var hash = HashPassword(plainTextPassword, salt);

            return new Password(hash, salt);
        }

        public static Password Create(string plainTextPassword, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword))
            {
                throw new InvalidPasswordException("Password is required");
            }

            if (plainTextPassword.Length < MinLength || plainTextPassword.Length > MaxLength)
            {
                throw new InvalidPasswordException($"Password must be between {MinLength} and {MaxLength} characters long");
            }

            if (!PasswordRegex.IsMatch(plainTextPassword))
            {
                throw new InvalidPasswordException("Password is invalid");
            }

            var hash = HashPassword(plainTextPassword, salt);

            return new Password(hash, salt);
        }


        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            const int iterations = 10000;
            const int hashLength = 32;
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(hashLength);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BitConverter.ToString(Hash);
            yield return BitConverter.ToString(Salt);
        }
    }
}
