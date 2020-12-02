using System.Linq;

namespace Infrastructure.Ddd
{
    public interface IFilter<TQueryable, TPredicate>
    {
        IQueryable<TQueryable> Filter(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}