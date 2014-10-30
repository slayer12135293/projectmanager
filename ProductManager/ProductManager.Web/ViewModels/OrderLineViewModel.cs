namespace ProductManager.Web.ViewModels
{
    public class OrderLineViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal ItemPrice { get; set; }
        public string ProductName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}