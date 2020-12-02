using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteStringEntityHandlerBase<TEntity, TCommand> : DeleteEntityHandlerBase<string, TEntity, TCommand>
        where TCommand : ICommand, IHasId<string>
        where TEntity : class, IHasId<string>
    {
    }
}