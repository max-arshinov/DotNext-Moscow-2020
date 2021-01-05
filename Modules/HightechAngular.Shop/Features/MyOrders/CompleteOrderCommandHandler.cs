using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Workflow;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class CompleteOrderCommandHandler : ICommandHandler<CompleteOrderContext, Task<CommandResult<OrderStatus>>>
    {
        public async Task<CommandResult<OrderStatus>> Handle(CompleteOrderContext input)
        {
            if (input.Entity.Status != OrderStatus.Shipped)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            input.Entity.BecomeComplete();
            await Task.Delay(500); // Third-party API call
            return input.Entity.Status;
        }
    }
}