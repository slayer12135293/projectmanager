using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProductManager.Web.ViewModels
{
    public class CreateSubCategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}