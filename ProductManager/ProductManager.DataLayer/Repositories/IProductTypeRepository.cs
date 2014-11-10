using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
    }

    public class ProductTypeRepository : EfRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(CategoryDb dbContext) : base(dbContext)
        {
        }
    }

}