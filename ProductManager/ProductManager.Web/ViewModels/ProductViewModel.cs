namespace ProductManager.Web.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string ImageUrl { get; set; }
        public decimal CurrentDiscount { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsNewProduct { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public int SubCategoryId { get; set; }
    }
}
