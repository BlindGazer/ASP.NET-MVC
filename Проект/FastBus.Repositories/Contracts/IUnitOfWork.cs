using System;
using FastBus.DAL.Concrete;

namespace FastBus.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        FastBusDbContext Context { get; }
        void RollBack();
        void SaveChanges();
        IGenericRepository<TEntity> GetRepostirory<TEntity>() where TEntity : class;
    }
}
