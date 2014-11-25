using System.Data.Entity;
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
            var productType = DbSet.FirstOrDefault(x => x.Id == id);
            ProductType result = productType ?? new ProductType() {Id = 9999, Name = "Type has been removed", Description = "Please choose a new type"};
            return result;
        }
    }

}