using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataLayer.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Catagories { get; }
        IQueryable<SubCategory> SubCatagories { get; }
        IQueryable<Product> Products { get; }
        void save();
        void RemoveSubCatagory(SubCategory subcatagory);
        void RemoveProduct(Product product);
    }

    public class CategoryRepository :DbContext, ICategoryRepository
    {
        public CategoryRepository()
            : base("ProductManagerConnection")
        {
        }
        public DbSet<Category> Catagories { get; set; }
        public DbSet<SubCategory> SubCatagories { get; set; }
        public DbSet<Product> Products { get; set; }
        

        IQueryable<Category> ICategoryRepository.Catagories
        {
            get { return Catagories; }
        }

        IQueryable<SubCategory> ICategoryRepository.SubCatagories
        {
            get { return SubCatagories; }
        }

        IQueryable<Product> ICategoryRepository.Products
        {
            get { return Products; }
        }

        void ICategoryRepository.save()
        {
            SaveChanges();
        }

        void ICategoryRepository.RemoveSubCatagory(SubCategory subcategory)
        {
            SubCatagories.Remove(subcategory);
            SaveChanges();
        }

        void ICategoryRepository.RemoveProduct(Product product)
        {
            Products.Remove(product);
            SaveChanges();
            
        }
    }


}
