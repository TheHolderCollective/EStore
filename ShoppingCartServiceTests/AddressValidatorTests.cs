using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
using Xunit;

namespace EStore_Tests
{
    public class AddressValidatorTests
    {
        [Fact]
        public void AddressValidator_IsValidTrueIfAddressIsValid()
        {
            // Arrange
            AddressValidator addressValidator = new AddressValidator();
            Address testAddress1 = new Address() { Country = "Canada", City = "Burnaby", Street = "Kingsway" };

            // Act
            bool result1 = addressValidator.IsValid(testAddress1);

            // Assert
            Assert.True(result1);
        }

        [Fact]
        public void AddressValidator_IsValidFalseIfAddressIsInvalid()
        {
            // Arrange
            AddressValidator addressValidator = new AddressValidator();
            Address testAddress2 = new Address() { Country = "USA", City = "", Street = "" };

            // Act
            bool result1 = addressValidator.IsValid(testAddress2);

            // Assert
            Assert.False(result1);
        }
    }
}
