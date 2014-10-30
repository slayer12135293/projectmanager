namespace ProductManager.Enity
{
    public class OrderLine 
    {
        public int Id { get; set; }
        public virtual Order Order {get;set;}
        public virtual int OrderId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual decimal ItemPrice { get; set; }
        public virtual decimal Width { get; set; }
        public virtual decimal Height { get; set; }
        public virtual int NumberOfItems { get; set; }
        public virtual int ProductId { get; set; }
        public virtual decimal UnitDiscount { get; set; }
    }
}