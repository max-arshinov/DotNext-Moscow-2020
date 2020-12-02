using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteLongEntityHandlerBaseAsync<TEntity, TCommand> : DeleteEntityHandlerBaseAsync<long, TEntity, TCommand>
        where TCommand : ICommand<Task>, IHasId<long>
        where TEntity : class, IHasId<long>
    {
    }
}