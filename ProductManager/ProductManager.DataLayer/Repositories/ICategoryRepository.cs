using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //IQueryable<SubCategory> GetSubCategories();
        
    }

    public class CategoryRepository :EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }


        //public IQueryable<SubCategory> GetSubCategories()
        //{
        //    //return DbContext.Set<Category>().SelectMany(x => x.SubCategories).AsQueryable();
        //}
    }
}
