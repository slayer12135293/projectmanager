using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    
    }

    public class OrderRepository :EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(CategoryDb dbContext)
            : base(dbContext)
        {
        }

      
    }
}