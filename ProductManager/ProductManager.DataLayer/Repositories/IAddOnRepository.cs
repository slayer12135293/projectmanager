using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IAddOnRepository : IRepository<AddOn>
    {
        Task<IEnumerable<AddOn>> GetAddOnsByProductType(int productTypeId);
    }

    public class AddOnRepository : EfRepository<AddOn>, IAddOnRepository
    {
        public AddOnRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<AddOn>> GetAddOnsByProductType(int productTypeId)
        {
            return await DbSet.Where(x => x.ProductTypeId == productTypeId).ToListAsync();
        }
    }
}