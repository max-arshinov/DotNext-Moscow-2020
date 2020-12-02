using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetLongEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<long, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<long>
    {
        protected GetLongEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}