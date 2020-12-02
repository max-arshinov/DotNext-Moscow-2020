using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrder, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IHandler<Order.Paid, Task<Order.Shipped>> _handler;
        private readonly IQueryable<Order> _orders;

        public ShipOrderCommandHandler(IHandler<Order.Paid, Task<Order.Shipped>> handler, 
            IQueryable<Order> orders)
        {
            _handler = handler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(ShipOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = await order.With((Order.Paid paidOrder) => _handler.Handle(paidOrder));
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}