using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Update
{
    public abstract class UpdateLongEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<long, TEntity, TCommand> 
        where TEntity : class, IHasId<long>
        where TCommand : ICommand, IHasId<long>
    {
    }
}