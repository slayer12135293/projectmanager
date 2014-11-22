using System.Security.Cryptography.X509Certificates;
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<AddOn> AddOns { get; set; }
        public DbSet<PricePlan> PricePlans { get; set; }
        public DbSet<PriceUnit> PriceUnits { get; set; }
        public System.Data.Entity.DbSet<ProductManager.Enity.Buyer> Buyers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasMany(x=>x.SubCategories).WithRequired(c => c.Category).HasForeignKey(c => c.CategoryId).WillCascadeOnDelete(true);
            modelBuilder.Entity<SubCategory>().HasMany(x=>x.Products).WithRequired(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).WillCascadeOnDelete(true);
            modelBuilder.Entity<ProductType>().HasMany(x=>x.AddOns).WithRequired(s=>s.ProductType).HasForeignKey(c=>c.ProductTypeId).WillCascadeOnDelete(true);
            modelBuilder.Entity<OrderLine>().HasMany(x => x.OrderLineAddOns).WithRequired(s => s.OrderLine).HasForeignKey(c => c.OrderLineId).WillCascadeOnDelete(true);
            modelBuilder.Entity<PricePlan>().HasMany(x=>x.PriceUnits).WithOptional().WillCascadeOnDelete(true);
            modelBuilder.Entity<Buyer>().HasMany(x => x.Orders).WithOptional().HasForeignKey(c => c.BuyerId);
            //modelBuilder.Entity<Customer>().HasMany(c => c.Categories).WithRequired(x=>x.Customer).HasForeignKey(y=>y.CustomerId).WillCascadeOnDelete(true);
            //modelBuilder.Entity<Customer>().HasMany(c => c.SubCategories).WithRequired(x=>x.Customer).HasForeignKey(y=>y.CustomerId).WillCascadeOnDelete(true);
            //modelBuilder.Entity<Customer>().HasMany(c => c.Products).WithRequired(x=>x.Customer).HasForeignKey(y=>y.CustomerId).WillCascadeOnDelete(true);


            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static CategoryDb Create()
        {
            return new CategoryDb();
        }

      

    }
}
