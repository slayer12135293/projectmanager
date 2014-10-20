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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(x=>x.SubCategories).WithRequired(c => c.Category).HasForeignKey(c => c.CategoryId).WillCascadeOnDelete(true);
            modelBuilder.Entity<SubCategory>().HasMany(x=>x.Products).WithRequired(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).WillCascadeOnDelete(true);
        }
    }
}
