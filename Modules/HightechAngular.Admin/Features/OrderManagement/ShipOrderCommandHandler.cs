using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Workflow;
using JetBrains.Annotations;

namespace HightechAngular.Admin.Features.OrderManagement
{
    [UsedImplicitly]
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrderContext, Task<CommandResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public ShipOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }

        public async Task<CommandResult<OrderStatus>> Handle(ShipOrderContext input)
        {
            await Task.Delay(1000);
            var result = input.Order.With((Order.Paid paidOrder) => paidOrder.BecomeShipped());
            return result != null
                ? new CommandResult<OrderStatus>(result.EligibleStatus)
                : new CommandResult<OrderStatus>(FailureInfo.Invalid("Order is in invalid state"));
        }
    }
}