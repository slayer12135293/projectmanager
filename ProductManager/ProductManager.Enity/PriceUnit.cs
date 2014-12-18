namespace ProductManager.Enity
{
    public class PriceUnit:BaseEntity
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Price { get; set; }
    }
}