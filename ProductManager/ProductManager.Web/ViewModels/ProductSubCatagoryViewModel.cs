
using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
