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

        public async  Task<IQueryable<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var currentCategory = await DbContext.Set<Category>().SingleAsync(x => x.Id == categoryId);
            return currentCategory.SubCategories.AsQueryable();
        }

        public async Task<SubCategory> GetSubCategoryByIds(int categoryId, int subCategoryId)
        {
            var currentCategory = await DbContext.Set<Category>().SingleAsync(x => x.Id == categoryId);
            return currentCategory.SubCategories.Single(y => y.Id == subCategoryId);
        }
    }
}