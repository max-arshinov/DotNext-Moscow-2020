using System.Linq;

namespace Infrastructure.Cqrs
{
    public interface ISorter<TQueryable, in TPredicate>
    {
        IOrderedQueryable<TQueryable> Sort(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}