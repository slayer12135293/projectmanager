using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductManager.Enity
{
    public class Product : BaseEntity
    {
        [Required]
        [Index]
        [MaxLength(256)]
        public string ProductCode { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string ImageUrl { get; set; }
        public decimal CurrentDiscount { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColoCode { get; set; }
        public Company OwnedBy { get; set; }
    }




    
}
