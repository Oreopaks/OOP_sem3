using Xunit;
using VendingMachine;

namespace VendingMachine.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_Test()
        {
            Product product = new Product("Кока-кола", 50m, 10, 1);

            Assert.Equal("Кока-кола", product.Name);
            Assert.Equal(50m, product.Price);
            Assert.Equal(10, product.Quantity);
            Assert.Equal(1, product.Code);
        }

        [Fact]
        public void ProductToString_Test()
        {
            Product product = new Product("Кока-кола", 50m, 10, 1);

            string text = product.ToString();

            Assert.Contains("Кока-кола", text);
            Assert.Contains("50", text);
        }

        [Fact]
        public void ChangeProductQuantity_Test()
        {
            Product product = new Product("Сок", 40m, 5, 2);

            product.Quantity = 15;

            Assert.Equal(15, product.Quantity);
        }
    }
}
