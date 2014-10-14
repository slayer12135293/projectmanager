using ProductManager.Enity;
using System.Collections.Generic;

namespace ProductManager.Web.ViewModels
{
    public class CategoryViewModel
    {
        public ICollection<ProductSubCatagoryViewMode> SubCategories { get; set; }
        public Category Category { get; set; }
    }
}
