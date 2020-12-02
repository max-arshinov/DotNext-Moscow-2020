using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Read
{
    public abstract class GetOneByStringIdQueryHandlerBaseAsync<TQuery, TEntity, TDto> :
        GetOneByIdQueryHandlerBaseAsync<string, TQuery, TEntity, TDto>
        where TQuery : IQuery<Task<TDto>>, IHasId<string>
        where TDto : class, IHasId<string>
    {
        protected GetOneByStringIdQueryHandlerBaseAsync(IQueryable<TEntity> queryable) : base(queryable)
        {
        }
    }
}