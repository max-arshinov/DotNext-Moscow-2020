using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteIntEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<int, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<int>
        where TEntity : class, IHasId<int>
    {
    }
}