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
    public class DisputeOrderCommandHandler : ICommandHandler<DisputeOrderContext, Task<CommandResult<OrderStatus>>>
    {
        public async Task<CommandResult<OrderStatus>> Handle(DisputeOrderContext input)
        {
            var result = input.Entity.With((Order.Shipped newOrder) => newOrder.BecomeDisputed());
            if (result == null)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }

            await Task.Delay(500); // Third-party API call
            return result.EligibleStatus;
        }
    }
}