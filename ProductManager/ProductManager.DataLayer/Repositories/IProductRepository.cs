using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IQueryable<Product>> GetProductsFromSubCategory(int categoryId, int subCategoryId);
        Task<Product> GetProductByIds(int categoryId, int subCategoryId, int productId);
        Task DeleteProductById(int categoryId, int subCategoryId, int productId);
        Task UpdateProductById(int categoryId, int subCategoryId, Product updatedProduct);
        Task AddProduct(int categoryId, int subCategoryId, Product updatedProduct);

    }

    public class ProductRepository : EfRepository<Product>, IProductRepository
    {

        public ProductRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

        public async Task<IQueryable<Product>> GetProductsFromSubCategory(int categoryId, int subCategoryId)
        {
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            return currentSubCategory.Products.AsQueryable();
        }

        public async Task<Product> GetProductByIds(int categoryId, int subCategoryId, int productId)
        {
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            return currentSubCategory.Products.Single(x => x.Id == productId);
        }

        public async Task DeleteProductById(int categoryId, int subCategoryId, int productId)
        {
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            var currentProduct = currentSubCategory.Products.Single(y => y.Id == productId);
            ((CategoryDb)DbContext).Products.Remove(currentProduct);
            await DbContext.SaveChangesAsync();

        }

        public async Task UpdateProductById(int categoryId, int subCategoryId, Product updatedProduct)
        {
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            var currentProduct = currentSubCategory.Products.Single(x => x.Id == updatedProduct.Id);
            
            Mapper.Map(updatedProduct,currentProduct);
            await DbContext.SaveChangesAsync();
        }

        public async Task AddProduct(int categoryId, int subCategoryId, Product product)
        {
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            currentSubCategory.Products.Add(product);
            await DbContext.SaveChangesAsync();

        }

        private async Task<SubCategory> GetCurrentSubCategory(int categoryId, int subCategoryId)
        {
            var currentCategory = await DbContext.Set<Category>().SingleAsync(x => x.Id == categoryId);
            var currentSubCategory = currentCategory.SubCategories.Single(y => y.Id == subCategoryId);
            return currentSubCategory;
        }
    }

}