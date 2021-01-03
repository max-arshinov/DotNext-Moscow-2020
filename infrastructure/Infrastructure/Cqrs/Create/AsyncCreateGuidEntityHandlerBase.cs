using System;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class AsyncCreateGuidEntityHandlerBase<TCommand, TEntity> :
        AsyncCreateEntityHandlerBase<Guid, TCommand, TEntity>
        where TEntity : class, IHasId<Guid>
        where TCommand : ICommand<Task<Guid>> { }
}