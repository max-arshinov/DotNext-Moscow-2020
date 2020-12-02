using System.Linq;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetOneByLongIdQueryHandlerBase<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBase<long, TQuery, TEntity, TDto>
        where TQuery : IQuery<TDto>, IHasId<long>
        where TDto : class, IHasId<long>
    {
        protected GetOneByLongIdQueryHandlerBase(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}