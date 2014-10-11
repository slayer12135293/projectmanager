
using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
