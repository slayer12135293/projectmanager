using System.Collections.Generic;
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
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string ImageUrl { get; set; }
        public decimal CurrentDiscount { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColoCode { get; set; }
        public int ProductType { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<ProdctTypeViewModel> ProductTypeViewModels { get; set; } 
    }

    public class ProdctTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}