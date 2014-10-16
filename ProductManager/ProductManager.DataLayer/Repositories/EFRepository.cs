﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DataLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
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

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
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
                DbContext.SaveChangesAsync();
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
            DbContext.SaveChangesAsync();
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

        public async void Remove(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return; // not found; assume already deleted.
            Remove(entity);
        }
    }
}
