using System;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteGuidEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<Guid, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<Guid>
        where TEntity : class, IHasId<Guid>
    {
    }
}