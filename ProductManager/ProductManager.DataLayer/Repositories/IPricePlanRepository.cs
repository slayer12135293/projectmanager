using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IPricePlanRepository : IRepository<PriceList>
    {
    }

    public class PricePlanRepository : EfRepository<PriceList>, IPricePlanRepository
    {
        public PricePlanRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

    }
}