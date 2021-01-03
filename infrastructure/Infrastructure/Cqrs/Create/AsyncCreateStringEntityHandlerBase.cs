using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;

namespace Infrastructure.Cqrs.Create
{
    public abstract class AsyncCreateStringEntityHandlerBase<TCommand, TEntity> :
        AsyncCreateEntityHandlerBase<string, TCommand, TEntity>
        where TEntity : class, IHasId<string>
        where TCommand : ICommand<Task<string>> { }
}