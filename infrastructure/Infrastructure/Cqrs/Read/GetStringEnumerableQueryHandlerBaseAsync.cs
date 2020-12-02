using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetStringEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<string, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<string>
    {
        protected GetStringEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}