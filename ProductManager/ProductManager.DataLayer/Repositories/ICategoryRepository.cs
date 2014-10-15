using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public class CategoryRepository :EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }
    }
}
