using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Cqrs.Read
{
    /// <summary>
    /// Base handler for async get entity by Id
    /// </summary>
    /// <typeparam name="TKey">Id type</typeparam>
    /// <typeparam name="TQuery">Input query</typeparam>
    /// <typeparam name="TEntity">Domain entity</typeparam>
    /// <typeparam name="TDto">Output dto</typeparam>
    public abstract class GetOneByIdQueryHandlerBaseAsync<TKey, TQuery, TEntity, TDto> :
        GetOneByIdMapBase<TQuery, TEntity, TDto>,
        IQueryHandler<TQuery, Task<TDto>>
        where TQuery : IQuery<Task<TDto>>, IHasId<TKey>
        where TDto : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IQueryable<TEntity> _queryable;

        protected GetOneByIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable)
        {
            _queryable = queryable;
        }
        
        public async Task<TDto> Handle(TQuery input) =>
            await Map(_queryable, input).FirstOrDefaultAsync(x => x.Id.Equals(input.Id));
    }
}