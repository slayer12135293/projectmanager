using System.ComponentModel;
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
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageUrl { get; set; }
        public int UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColoCode { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int ProductTypeId { get; set; }
        [DisplayName("Price Plan")]
        public virtual int PricePlanId { get; set; }

    }




}
