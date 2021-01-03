using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Workflow;
using JetBrains.Annotations;

namespace HightechAngular.Admin.Features.OrderManagement
{
    [UsedImplicitly]
    public class PayOrderCommandHandler : ICommandHandler<PayOrderContext, Task<CommandResult<OrderStatus>>>
    {
        public async Task<CommandResult<OrderStatus>> Handle(PayOrderContext input)
        {
            await Task.Delay(1000);
            var result = input.Order.With((Order.New newOrder) => newOrder.BecomePaid());
            return result != null
                ? new CommandResult<OrderStatus>(result.EligibleStatus)
                : new CommandResult<OrderStatus>(FailureInfo.Invalid("Order is in invalid state"));
        }
    }
}