using ProductManager.Enity;
using System.Data.Entity;

namespace ProductManager.DataLayer
{


    public class CategoryDb : DbContext
    {
        public CategoryDb()
            : base("ProductManagerConnection")
        {            
        }
        public DbSet<Category> Catagories { get; set; }
        public DbSet<SubCategory> SubCatagories { get; set; }
        public DbSet<Product> Products { get; set; }        
        
    }
}
