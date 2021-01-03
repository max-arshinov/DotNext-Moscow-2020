using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class
        AsyncDeleteStringEntityHandlerBase<TEntity, TCommand> : AsyncDeleteEntityHandlerBase<string, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<string>
        where TEntity : class, IHasId<string> { }
}