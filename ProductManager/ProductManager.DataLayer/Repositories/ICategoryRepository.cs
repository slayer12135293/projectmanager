using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataLayer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<SubCategory> GetSubCategories();
    }

    public class CategoryRepository :EFRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }


        public IQueryable<SubCategory> GetSubCategories()
        {
            return DbContext.Set<Category>().SelectMany(x => x.SubCategories).AsQueryable();
        }
    }


}
