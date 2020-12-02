using System;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Update
{
    public abstract class UpdateGuidEntityHandlerBase<TEntity, TCommand>: 
        UpdateEntityHandlerBase<Guid, TEntity, TCommand> 
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand, IHasId<Guid>
    {
    }
}