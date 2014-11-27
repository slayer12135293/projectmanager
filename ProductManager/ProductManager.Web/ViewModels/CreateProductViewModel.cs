using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.Web.ViewModels
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string ProductCode { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageUrl { get; set; }
        public int UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColoCode { get; set; }
        [DisplayName("Product Type")]
        public int? ProductTypeId { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Price Plan")]
        public int? PricePlanId { get; set; }
        public IEnumerable<ProdctTypeViewModel> ProductTypeViewModels { get; set; } 
    }

    public class EditProductViewModel : CreateProductViewModel
    {
        public IEnumerable<PricePlanDropDownViewModel> PricePlanViewModels { get; set; }
        public bool UsingPricePlan { get; set; }
    }


}