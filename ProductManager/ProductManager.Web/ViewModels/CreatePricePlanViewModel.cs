using System.Collections.Generic;
using System.ComponentModel;
using ProductManager.Enity;

namespace ProductManager.Web.ViewModels
{
    public class CreatePricePlanViewModel
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; } 

    }
}