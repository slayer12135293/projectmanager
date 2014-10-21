using Microsoft.AspNet.Identity.EntityFramework;
using ProductManager.Enity;
using System.Data.Entity;

namespace ProductManager.DataLayer
{

    public class CategoryDb : IdentityDbContext<ApplicationUser>
    {
        public CategoryDb()
            : base("ProductManagerConnection", throwIfV1Schema: false)
        {            
        }

        public DbSet<Category> Catagories { get; set; }
        public DbSet<SubCategory> SubCatagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(x=>x.SubCategories).WithRequired(c => c.Category).HasForeignKey(c => c.CategoryId).WillCascadeOnDelete(true);
            modelBuilder.Entity<SubCategory>().HasMany(x=>x.Products).WithRequired(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).WillCascadeOnDelete(true);
        }

        public static CategoryDb Create()
        {
            return new CategoryDb();
        }
    }
}
