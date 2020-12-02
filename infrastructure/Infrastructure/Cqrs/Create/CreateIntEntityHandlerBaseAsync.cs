using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class CreateIntEntityHandlerBaseAsync<TCommand, TEntity> :
        CreateEntityHandlerBaseAsync<int, TCommand, TEntity>
        where TEntity : class, IHasId<int>
        where TCommand : ICommand<Task<int>>
    {
    }
}