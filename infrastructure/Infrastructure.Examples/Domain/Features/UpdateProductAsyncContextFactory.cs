using System.Threading.Tasks;
using Infrastructure.OperationContext;

namespace Infrastructure.Examples.Domain.Features
{
    public class UpdateProductAsyncContextFactory: 
        IAsyncOperationContextFactory<UpdateProductAsync, UpdateProductAsyncContext>
    {
        public Task<UpdateProductAsyncContext> BuildAsync(UpdateProductAsync request)
        {
            return Task.FromResult(new UpdateProductAsyncContext(request));
        }
    }
}