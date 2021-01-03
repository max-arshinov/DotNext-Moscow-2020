using System.Linq;

namespace Infrastructure.Cqrs
{
    public interface IFilter<TQueryable, TPredicate>
    {
        IQueryable<TQueryable> Filter(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}