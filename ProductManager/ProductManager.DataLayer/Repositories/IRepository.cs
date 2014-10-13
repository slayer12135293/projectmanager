using System.Linq;

namespace ProductManager.DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(int id);
    }
}
