using System;

namespace FastBus.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void RollBack();
        void SaveChanges();
        IGenericRepository<TEntity> GetRepostirory<TEntity>() where TEntity : class;
    }
}
