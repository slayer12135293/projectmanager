using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IQueryable<Product>> GetProductsFromSubCategory(int categoryId, int subCategoryId);
        Task<Product> GetProductByIds(int categoryId, int subCategoryId, int productId);
        Task DeleteProductById(int categoryId, int subCategoryId, int productId);
        Task UpdateProductById(int categoryId, int subCategoryId, Product updatedProduct);

    }

    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductRepository(CategoryDb dbContext, ICategoryRepository categoryRepository) : base(dbContext)
        {
            _categoryRepository = categoryRepository;
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
            var currentCategory = await _categoryRepository.GetByIdAsync(categoryId);
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId); 
            var currentProduct = currentSubCategory.Products.Single(y => y.Id == productId);
            currentSubCategory.Products.Remove(currentProduct);
            _categoryRepository.Update(currentCategory);
            
        }

        public async Task UpdateProductById(int categoryId, int subCategoryId, Product updatedProduct)
        {
            var currentCategory = await _categoryRepository.GetByIdAsync(categoryId);
            var currentSubCategory = await GetCurrentSubCategory(categoryId, subCategoryId);
            var currentProduct = currentSubCategory.Products.Single(x => x.Id == updatedProduct.Id);
            currentProduct.Height = updatedProduct.Height;
            currentProduct.ImageUrl = updatedProduct.ImageUrl;
            currentProduct.IsNewProduct = updatedProduct.IsNewProduct;
            currentProduct.Name = updatedProduct.Name;
            currentProduct.OwnedBy = updatedProduct.OwnedBy;
            currentProduct.ProductCode = updatedProduct.ProductCode;
            currentProduct.UnitPrice = updatedProduct.UnitPrice;
            currentProduct.ColoCode = updatedProduct.ColoCode;
            currentProduct.ColorName = updatedProduct.ColorName;
            currentProduct.CurrentDiscount = updatedProduct.CurrentDiscount;
            _categoryRepository.Update(currentCategory);


        }

        private async Task<SubCategory> GetCurrentSubCategory(int categoryId, int subCategoryId)
        {
            var currentCategory = await DbContext.Set<Category>().SingleAsync(x => x.Id == categoryId);
            var currentSubCategory = currentCategory.SubCategories.Single(y => y.Id == subCategoryId);
            return currentSubCategory;
        }
    }

}