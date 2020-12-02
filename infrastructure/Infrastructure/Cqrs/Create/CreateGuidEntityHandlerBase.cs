using System;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class CreateGuidEntityHandlerBase<TCommand, TEntity> :
        CreateEntityHandlerBase<Guid, TCommand, TEntity>
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand<Guid>
    {
    }
}