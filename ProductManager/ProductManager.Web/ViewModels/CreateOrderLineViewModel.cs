using System.ComponentModel.DataAnnotations;

namespace ProductManager.Web.ViewModels
{
    public class CreateOrderLineViewModel
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }
    }
}