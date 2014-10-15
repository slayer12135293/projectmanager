namespace ProductManager.Enity
{
    public class OrderLine 
    {
        public int Id { get; set; }
        public virtual Order Order {get;set;}
        public decimal ItemPrice { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public int NumberOfItems { get; set; }
        public Product Product { get; set; }
        public decimal UnitDiscount { get; set; }
    }
}