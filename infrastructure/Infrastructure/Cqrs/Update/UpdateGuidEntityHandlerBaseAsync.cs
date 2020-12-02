using System;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Update
{
    public abstract class UpdateGuidEntityHandlerBaseAsync<TEntity, TCommand> :
        UpdateEntityHandlerBaseAsync<Guid, TEntity, TCommand>
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand<Task>, IHasId<Guid>
    {
    }
}