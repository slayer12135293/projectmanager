using System.Collections.Generic;
using System.Linq;
using ProductManager.Enity;

namespace ProductManager.Entity.UnitTests
{
    public class Basket
    {
        private readonly List<CartItem> _products = new List<CartItem>();
        private int _discount;
        private decimal _assemblageCost;
        private const decimal AssemblyCostPerItem = 140;

        public decimal GetTotal()
        {
            return _products.Sum(x => x.Product.UnitPrice * x.NumberOfItems) - _discount + _assemblageCost;
        }

        public void UpdateAssemblage()
        {
            _assemblageCost = _products.Sum(x => x.NumberOfItems * AssemblyCostPerItem);
        }

        public void AddProduct(Product product, int height, int width, int numberOfItems)
        {
            _products.Add(new CartItem(product, height, width, numberOfItems));
           // Total += GetTotal();
        }

        public void SetDiscount(int discount)
        {
            _discount = discount;
        }

        public void UpdateQuantity(int cartItemId, int numberOfItems)
        {
            var cartItem = _products.FirstOrDefault(x => x.Id == cartItemId);

            if (cartItem == null)
                return;

            cartItem.NumberOfItems = numberOfItems;
        }
    }
}