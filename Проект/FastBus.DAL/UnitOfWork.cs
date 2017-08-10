using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using FastBus.DAL.Contracts;
using FastBus.Persistence;
using NLog;

namespace FastBus.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private FastBusDbContext _context;
        public UnitOfWork()
        {
            _context = new FastBusDbContext();
        }

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>(); 

        public void RollBack()
        {
            var changedEntries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                _logger.Error("Context.SaveChanges raised an error: {0}", sb);
                // Add the original exception as the innerException
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }

        public IGenericRepository<TEntity> GetRepostirory<TEntity>() where TEntity : class
        {
            if(!_repositories.ContainsKey(typeof(TEntity)))
            {
                _repositories.Add(typeof(TEntity), new GenericRepostiory<TEntity>(_context));
            }
            return _repositories[typeof(TEntity)] as GenericRepostiory<TEntity>;
        }

        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
        }

    }
}
