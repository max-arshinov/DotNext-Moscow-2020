using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class CreateIntEntityHandlerBase<TCommand, TEntity> :
        CreateEntityHandlerBase<int, TCommand, TEntity>
        where TEntity : class, IHasId<int>
        where TCommand : ICommand<int>
    {
    }
}