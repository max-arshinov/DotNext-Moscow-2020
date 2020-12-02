using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class DisputeOrderCommandHandler: ICommandHandler<DisputeOrder, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IHandler<Order.Shipped, Task<Order.Dispute>> _handler;
        private readonly IQueryable<Order> _orders;

        public DisputeOrderCommandHandler(IHandler<Order.Shipped, Task<Order.Dispute>> handler, 
            IQueryable<Order> orders)
        {
            _handler = handler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(DisputeOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = await order.With((Order.Shipped shippedOrder) => _handler.Handle(shippedOrder));
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}