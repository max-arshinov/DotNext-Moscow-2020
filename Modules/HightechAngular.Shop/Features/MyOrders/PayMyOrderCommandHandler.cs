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
    public class PayMyOrderCommandHandler : ICommandHandler<PayMyOrder, Task<CommandResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public PayMyOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }

        public async Task<CommandResult<OrderStatus>> Handle(PayMyOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            await Task.Delay(1000);
            var result = order.With((Order.New newOrder) => newOrder.BecomePaid());
            return result != null
                ? new CommandResult<OrderStatus>(result.EligibleStatus)
                : new CommandResult<OrderStatus>(FailureInfo.Invalid("Order is in invalid state"));
        }
    }
}