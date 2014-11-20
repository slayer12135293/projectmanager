using ProductManager.Enity;

namespace ProductManager.Entity.UnitTests
{
    public class CartItem
    {
        public CartItem(Product product, int height, int width, int numberOfItems)
        {
            Product = product;
            Height = height;
            Width = width;
            NumberOfItems = numberOfItems;
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int NumberOfItems { get; set; }
    }
}