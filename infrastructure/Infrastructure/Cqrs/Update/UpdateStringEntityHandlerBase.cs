using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Update
{
    public abstract class UpdateStringEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<string, TEntity, TCommand> 
        where TEntity : class, IHasId<string>
        where TCommand : ICommand, IHasId<string>
    {
    }
}