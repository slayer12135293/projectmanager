using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<IQueryable<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
        Task<SubCategory> GetSubCategoryByIds(int categoryId, int subCategoryId);
        Task UpdateSubCategoryByIds(int categoryId, int subCategoryId, SubCategory updatedSubCategory);
        Task AddSubCategory(int categoryId,SubCategory subCategory);
        Task DeleteSubCategoryByIds(int categoryId, int subCategoryId);
    }
}