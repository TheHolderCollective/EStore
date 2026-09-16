using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using Xunit;

namespace EStore_Tests
{
    public class ShippingCalculatorTests
    {
        [Theory]
        [InlineData(ShippingMethod.Standard, 0)]
        [InlineData(ShippingMethod.Express, 0)]
        [InlineData(ShippingMethod.Priority, 0)]
        [InlineData(ShippingMethod.Expedited, 0)]
        public void ShippingCalculator_ShippingCostShouldBeZeroForEmptyCart(ShippingMethod shippingMethod, double expected)
        {
            //Arrange
            var shippingCalculator = new ShippingCalculator();

            var shoppingCart = new Cart();
            shoppingCart.ShippingMethod = shippingMethod;
            shoppingCart.ShippingAddress = new Address() { Country = "USA", City = "Houston", Street = "Lowes" };

            //Act
            double result = shippingCalculator.CalculateShippingCost(shoppingCart);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(ShippingMethod.Standard, 1, 1.0)]
        [InlineData(ShippingMethod.Expedited, 1, 1.2)]
        [InlineData(ShippingMethod.Priority, 1, 2.0)]
        [InlineData(ShippingMethod.Express, 1, 2.5)]
        public void ShippingCalculator_ShippingCostCorrectForOneItemSameCity(ShippingMethod shippingMethod, uint itemQuantity, double expected)
        {
            //Arrange
            var shippingCalculator = new ShippingCalculator();

            Cart shoppingCart = new Cart();
            shoppingCart.ShippingMethod = shippingMethod;
            shoppingCart.Items.Add(new Item() { ProductId = "123", ProductName = "XYZ", Quantity = itemQuantity, Price = 1 });
            shoppingCart.ShippingAddress = new Address() { Country = "USA", City = "Dallas", Street = "Lowes" };

            //Act
            double result = shippingCalculator.CalculateShippingCost(shoppingCart);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(ShippingMethod.Standard, 1, 1.0 * 2)]
        [InlineData(ShippingMethod.Expedited, 1, 1.2 * 2)]
        [InlineData(ShippingMethod.Priority, 1, 2.0 * 2)]
        [InlineData(ShippingMethod.Express, 1, 2.5 * 2)]
        public void ShippingCalculator_ShippingCostCorrectForOneItemSameCountry(ShippingMethod shippingMethod, uint itemQuantity, double expected)
        {
            //Arrange
            var shippingCalculator = new ShippingCalculator();

            Cart shoppingCart = new Cart();
            shoppingCart.ShippingMethod = shippingMethod;
            shoppingCart.Items.Add(new Item() { ProductId = "110", ProductName = "ABC", Quantity = itemQuantity, Price = 1 });
            shoppingCart.ShippingAddress = new Address() { Country = "USA", City = "Baltimore", Street = "Jakes" };

            //Act
            double result = shippingCalculator.CalculateShippingCost(shoppingCart);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(ShippingMethod.Standard, 1, 1.0 * 15)]
        [InlineData(ShippingMethod.Expedited, 1, 1.2 * 15)]
        [InlineData(ShippingMethod.Priority, 1, 2.0 * 15)]
        [InlineData(ShippingMethod.Express, 1, 2.5 * 15)]
        public void ShippingCalculator_ShippingCostCorrectForOneItemInternational(ShippingMethod shippingMethod, uint itemQuantity, double expected)
        {
            //Arrange
            var shippingCalculator = new ShippingCalculator();

            Cart shoppingCart = new Cart();
            shoppingCart.ShippingMethod = shippingMethod;
            shoppingCart.Items.Add(new Item() { ProductId = "110", ProductName = "ABC", Quantity = itemQuantity, Price = 1 });
            shoppingCart.ShippingAddress = new Address() { Country = "Canada", City = "Toronto", Street = "Parks" };

            //Act
            double result = shippingCalculator.CalculateShippingCost(shoppingCart);

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
