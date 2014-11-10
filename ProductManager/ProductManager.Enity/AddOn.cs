namespace ProductManager.Enity
{
    public class AddOn : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int Price { get; set; }
    }
}