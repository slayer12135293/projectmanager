using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManager.DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<T> GetByIdAsync(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task RemoveAsync(T entity);
        Task Remove(int id);
    }
}
