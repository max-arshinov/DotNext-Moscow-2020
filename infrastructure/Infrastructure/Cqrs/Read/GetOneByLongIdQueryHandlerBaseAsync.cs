using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetOneByLongIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<long, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<long>
        where TDto : class, IHasId<long>
    {
        protected GetOneByLongIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}