using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetGuidEnumerableQueryHandlerBaseAsync<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBaseAsync<Guid, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<Task<IEnumerable<TListItem>>>
        where TListItem : IHasId<Guid>
    {
        protected GetGuidEnumerableQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}