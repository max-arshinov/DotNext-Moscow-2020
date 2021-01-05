using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Workflow;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class PayMyOrderCommandHandler : ICommandHandler<PayMyOrderContext, Task<CommandResult<OrderStatus>>>
    {
        public async Task<CommandResult<OrderStatus>> Handle(PayMyOrderContext input)
        {
            if (input.Entity.Status != OrderStatus.New)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            input.Entity.BecomePaid();
            await Task.Delay(500); // Third-party API call
            return input.Entity.Status;
        }
    }
}