using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IList<Product>> GetProductsFromSubCategory( int subCategoryId);

    }

    public class ProductRepository : EfRepository<Product>, IProductRepository
    {

        public ProductRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }


        public async Task<IList<Product>> GetProductsFromSubCategory( int subCategoryId)
        {
            return await DbSet.Where(x => x.SubCategoryId == subCategoryId).ToListAsync();
        }
    }

}