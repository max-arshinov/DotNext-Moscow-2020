using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class AsyncDeleteIntEntityHandlerBase<TEntity, TCommand> : 
        AsyncDeleteEntityHandlerBase<int, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<int>
        where TEntity : class, IHasId<int> { }
}