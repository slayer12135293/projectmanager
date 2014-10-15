
using ProductManager.Enity;
using System.Collections.Generic;

namespace ProductManager.Web.ViewModels
{
    public class ProductSubCatagoryViewMode
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryDescription { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
