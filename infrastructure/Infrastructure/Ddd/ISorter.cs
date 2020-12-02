using System.Linq;

namespace Infrastructure.Ddd
{
    public interface ISorter<TQueryable, in TPredicate>
    {
        IOrderedQueryable<TQueryable> Sort(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}