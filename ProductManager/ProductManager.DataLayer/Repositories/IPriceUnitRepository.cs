using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IPriceUnitRepository : IRepository<PriceUnit>
    {
    }

    public class PriceUnitRepository : EfRepository<PriceUnit>, IPriceUnitRepository
    {
        public PriceUnitRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

    }

}