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
    public interface ICustomerRepository : IRepository<Customer> { }

    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(CategoryDb dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<Customer>();
        }
        protected DbContext DbContext { get; set; }
        protected DbSet<Customer> DbSet { get; set; }

        public IQueryable<Customer> GetAll()
        {
            return DbSet;
        }

        public IEnumerable<Customer> Get(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Customer> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Add(Customer entity)
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

        public async Task Update(Customer entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Customer entity)
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