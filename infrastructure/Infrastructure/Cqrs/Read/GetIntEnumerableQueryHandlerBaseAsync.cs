using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetIntEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<int, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<int>
    {
        protected GetIntEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}