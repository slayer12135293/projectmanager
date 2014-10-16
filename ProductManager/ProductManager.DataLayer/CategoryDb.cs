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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(x=>x.SubCategories).WithOptional().WillCascadeOnDelete(true);
            modelBuilder.Entity<SubCategory>().HasMany(x=>x.Products).WithOptional().WillCascadeOnDelete(true);
        }
    }
}
