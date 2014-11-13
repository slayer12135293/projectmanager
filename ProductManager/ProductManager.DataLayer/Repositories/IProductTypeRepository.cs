using System.Linq;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        ProductType GetById(int id);
    }

    public class ProductTypeRepository : EfRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(CategoryDb dbContext) : base(dbContext)
        {
        }

        public ProductType GetById(int id)
        {
            return DbSet.Single(x => x.Id == id);
        }
    }

}