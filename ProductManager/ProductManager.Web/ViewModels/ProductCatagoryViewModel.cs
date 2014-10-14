
using ProductManager.Enity;
using System.Collections.Generic;

namespace ProductManager.Web.ViewModels
{
    public class ProductCatagoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
