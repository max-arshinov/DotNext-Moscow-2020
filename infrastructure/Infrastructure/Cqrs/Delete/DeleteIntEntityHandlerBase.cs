using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteIntEntityHandlerBase<TEntity, TCommand> : DeleteEntityHandlerBase<int, TEntity, TCommand>
        where TCommand : ICommand, IHasId<int>
        where TEntity : class, IHasId<int>
    {
    }
}