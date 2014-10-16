using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public class SubCategoryRepository : EfRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

        public async Task<IQueryable<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var currentCategory = await GetCurrentCategory(categoryId);
            return currentCategory.SubCategories.AsQueryable();
        }

        public async Task<SubCategory> GetSubCategoryByIds(int categoryId, int subCategoryId)
        {
            var currentCategory = await GetCurrentCategory(categoryId);
            return currentCategory.SubCategories.Single(y => y.Id == subCategoryId);
        }

        public async Task UpdateSubCategoryByIds(int categoryId, int subCategoryId, SubCategory updatedSubCategory)
        {
            var currentCategory = await GetCurrentCategory(categoryId);
            var currentSubCategory = GetCurrentSubCategory(subCategoryId, currentCategory);
            currentSubCategory.Description = updatedSubCategory.Description;
            currentSubCategory.Name = updatedSubCategory.Name;
            await DbContext.SaveChangesAsync();
        }

        public async Task AddSubCategory(int categoryId, SubCategory subCategory)
        {
            var currentCategory = await GetCurrentCategory(categoryId);
            currentCategory.SubCategories.Add(subCategory);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteSubCategoryByIds(int categoryId, int subCategoryId)
        {
            var currentCategory = await GetCurrentCategory(categoryId);
            var currentSubCategory = GetCurrentSubCategory(subCategoryId, currentCategory);
            currentSubCategory.Products.ToList().ForEach(p => ((CategoryDb)DbContext).Products.Remove(p));
            ((CategoryDb)DbContext).SubCatagories.Remove(currentSubCategory);
            await DbContext.SaveChangesAsync();
        }

        private static SubCategory GetCurrentSubCategory(int subCategoryId, Category currentCategory)
        {
            return currentCategory.SubCategories.Single(x => x.Id == subCategoryId);
        }

        private async Task<Category> GetCurrentCategory(int categoryId)
        {
            return await DbContext.Set<Category>().SingleAsync(x => x.Id == categoryId);
        }
    }
}