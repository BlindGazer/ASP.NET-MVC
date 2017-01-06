using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using FastBus.Repositories.Contracts;

namespace FastBus.Repositories
{
    public class GenericRepostiory<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private DbContext DbContext { get; }

        private DbSet<TEntity> DbSet { get; }

        public IQueryable<TEntity> All => DbSet;

        public GenericRepostiory(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Deleted)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            entry.State = EntityState.Deleted;
        }

        public void Remove(object id)
        {
            var entity = FindById(id);
            if (entity == null) return;
            Remove(entity);
        }

        public bool Exists(object id)
        {
            return FindById(id) != null;
        }

        public TEntity FindById(object id)
        {
            return DbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> prop)
        {
            return DbSet.First(prop);
        }

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = All;
            foreach (var prop in includeProperties)
            {
                query.Include(prop);
            }
            return query;
        }

        public IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = All;
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
            return query;
        }
       
    }
}
