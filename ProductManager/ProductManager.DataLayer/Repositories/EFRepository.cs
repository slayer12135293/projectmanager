using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataLayer.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {

        public EFRepository(DbContext dbContext)
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

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
                DbContext.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Remove(T entity)
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
        }

        public void Remove(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Remove(entity);
        }
    }
}
