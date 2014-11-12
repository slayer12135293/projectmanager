using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ProductManager.Web.ViewModels
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
         [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }
         [HiddenInput(DisplayValue = false)]
        public int SubCategoryId { get; set; }
        public string ProductCode { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageUrl { get; set; }
        public int UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColoCode { get; set; }
        public int ProductTypeId { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Price Plan")]
        public int PricePlanId { get; set; }
        public IEnumerable<ProdctTypeViewModel> ProductTypeViewModels { get; set; } 
    }
}