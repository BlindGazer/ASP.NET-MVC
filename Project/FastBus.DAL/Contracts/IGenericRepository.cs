using System;
using System.Linq;
using System.Linq.Expressions;

namespace FastBus.DAL.Contracts
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates);
        T Get(Expression<Func<T, bool>> prop);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(object id);
        T FindById(object id);
        bool Exists(object id);
    }
}
