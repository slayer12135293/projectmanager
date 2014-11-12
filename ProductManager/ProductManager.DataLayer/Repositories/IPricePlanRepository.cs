using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IPricePlanRepository : IRepository<PricePlan>
    {
    }

    public class PricePlanRepository : EfRepository<PricePlan>, IPricePlanRepository
    {
        public PricePlanRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

    }
}