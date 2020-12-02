using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class CreateLongEntityHandlerBase<TCommand, TEntity> :
        CreateEntityHandlerBase<long, TCommand, TEntity>
        where TEntity : class, IHasId<long>
        where TCommand : ICommand<long>
    {
    }
}