using FluentAssertions;
using OpenSkinsApi.Modules.Skins.Domain.Exceptions;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using Xunit;

namespace OpenSkinsApi.Tests.Modules.Skins.Domain.ValueObjects
{
    public class NameTests
    {
        [Theory]
        [InlineData("Valid Name")]
        [InlineData("Name With Numbers 123")]
        public void Create_ValidName_ReturnsName(string validName)
        {
            var name = Name.Create(validName);

            name.Value.Should().Be(validName);
            name.Should().BeOfType<Name>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_InvalidName_ThrowsInvalidNameException(string invalidName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(invalidName));
            ex.Message.Should().Be("Name is required");
        }

        [Theory]
        [InlineData("A name that exceeds the maximum length of 100 characters. This is a very long name indeed, and it should trigger an exception when creating a Name object with it.")]
        [InlineData("A")]
        public void Create_InvalidLengthName_ThrowsInvalidNameException(string invalidName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(invalidName));
            ex.Message.Should().Be($"Name must be between {Name.MinLength} and {Name.MaxLength} characters long");
        }

        [Theory]
        [InlineData("Invalid Name$")]
        [InlineData("Name@With#Symbols")]
        public void Create_InvalidCharactersName_ThrowsInvalidNameException(string invalidName)
        {
            var ex = Assert.Throws<InvalidNameException>(() => Name.Create(invalidName));
            ex.Message.Should().Be("Name must contain only letters and numbers");
        }
    }
}
