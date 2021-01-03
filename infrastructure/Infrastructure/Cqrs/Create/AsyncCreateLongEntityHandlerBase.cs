using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class AsyncCreateLongEntityHandlerBase<TCommand, TEntity> :
        AsyncCreateEntityHandlerBase<long, TCommand, TEntity>
        where TEntity : class, IHasId<long>
        where TCommand : ICommand<Task<long>> { }
}