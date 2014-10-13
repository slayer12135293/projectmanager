using ProductManager.Enity;
using System.Data.Entity;

namespace ProductManager.DataLayer
{
    public class OrderDb : DbContext
    {
        public OrderDb()
            : base("ProductManagerConnection")
        {
        }
        public DbSet<Order> Orders { get;set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<ProductCatagory> ProductCatagories { get; set; }
    }
    

}
