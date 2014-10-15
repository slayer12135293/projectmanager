using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<IQueryable<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
    }
}