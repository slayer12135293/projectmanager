using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProductManager.Enity;

namespace ProductManager.Web.ViewModels
{
    public class CreatePricePlanViewModel
    {
        [Required]
        public string Name { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; } 
    }

    public class EditPricePlanViewModel : CreatePricePlanViewModel
    {
        public int Id { get; set; }
    }

}