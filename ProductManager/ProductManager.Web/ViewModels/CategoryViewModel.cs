using ProductManager.Enity;
using System.Collections.Generic;

namespace ProductManager.Web.ViewModels
{
    public class CategoryViewModel
    {
        public ICollection<ProductSubCatagoryViewMode> SubCategories { get; set; }
        public Category Category { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string BuyerName { get; set; }
    }
}
