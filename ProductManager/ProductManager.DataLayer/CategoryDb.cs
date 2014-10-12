using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
