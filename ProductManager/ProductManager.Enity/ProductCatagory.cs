namespace ProductManager.Enity
{
    public class ProductCatagory :BaseEntity
    {
        public ProductCatagory ParentCategory { get; set; }
        public string Description { get; set; }
    }
    
}