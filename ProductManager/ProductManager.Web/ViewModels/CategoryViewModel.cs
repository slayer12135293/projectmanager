using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Web.ViewModels
{
    public class CategoryViewModel
    {
        public ICollection<ProductSubCatagoryViewMode> SubCategories { get; set; }
        public Category category { get; set; }
    }
}
