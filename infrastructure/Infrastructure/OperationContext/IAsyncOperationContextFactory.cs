using System.Threading.Tasks;

namespace Infrastructure.OperationContext
{
    public interface IAsyncOperationContextFactory<in TRequest, TContext>
        where TContext: OperationContextBase<TRequest> 
        where TRequest : class
    {
        public Task<TContext> BuildAsync(TRequest request);
    }
}