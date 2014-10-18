using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
    }

    public class SubCategoryRepository : EfRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }


        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return await DbSet.Where(s => s.CategoryId == categoryId).ToListAsync();
        }
    }
}