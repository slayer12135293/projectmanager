using ProductManager.Enity;
using Xunit;

namespace ProductManager.Entity.UnitTests
{
    public class OrderTets
    {
        [Fact]
        public void ShouldCalculatePrice()
        {
            var basket = new Basket();
            basket.AddProduct(new Product(), 1, 1, 1);
            basket.AddProduct(new Product(), 1, 1, 1);

            var total = basket.GetTotal();
            
            
        }
    }
}
