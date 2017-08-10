using System.Linq;
using System.Linq.Expressions;

namespace FastBus.Services.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool descending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            var method = descending ? "OrderBy" : "OrderByDescending";
            var types = new[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
