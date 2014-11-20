namespace ProductManager.Web.ViewModels
{
    public class PricePlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductTypeName { get; set;}
        public int ProductTypeId { get; set; }
    }

}