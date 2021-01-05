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
            if (input.Order.Status != OrderStatus.New)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            input.Order.BecomePaid();
            await Task.Delay(500); // Third-party API call
            return input.Order.Status;
        }
    }
}