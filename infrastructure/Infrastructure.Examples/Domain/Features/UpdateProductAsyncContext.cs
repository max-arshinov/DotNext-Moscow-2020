using System.Threading.Tasks;
using Force.Cqrs;
using Infrastructure.OperationContext;

namespace Infrastructure.Examples.Domain.Features
{
    public class UpdateProductAsyncContext: OperationContextBase<UpdateProductAsync>, ICommand<Task>
    {
        public UpdateProductAsyncContext(UpdateProductAsync request) : base(request)
        {
        }
    }
}