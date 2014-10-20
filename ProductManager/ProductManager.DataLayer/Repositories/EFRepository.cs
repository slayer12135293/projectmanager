using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {

        public EfRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext missiong");
            }
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            await DbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return; // not found; assume already deleted.
            await RemoveAsync(entity);
        }
    }
}
