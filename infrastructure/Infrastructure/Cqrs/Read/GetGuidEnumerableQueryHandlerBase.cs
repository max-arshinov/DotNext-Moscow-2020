using System;
using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetGuidEnumerableQueryHandlerBase<TQuery, TEntity, TListItem> :
        GetEnumerableQueryHandlerBase<Guid, TQuery, TEntity, TListItem>
        where TQuery : class, IQuery<IEnumerable<TListItem>>
        where TListItem : IHasId<Guid>
    {
        protected GetGuidEnumerableQueryHandlerBase(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}