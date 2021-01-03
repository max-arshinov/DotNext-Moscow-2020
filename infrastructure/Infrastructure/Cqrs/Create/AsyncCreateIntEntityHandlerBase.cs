using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class AsyncCreateIntEntityHandlerBase<TCommand, TEntity> :
        AsyncCreateEntityHandlerBase<int, TCommand, TEntity>
        where TEntity : class, IHasId<int>
        where TCommand : ICommand<Task<int>> { }
}