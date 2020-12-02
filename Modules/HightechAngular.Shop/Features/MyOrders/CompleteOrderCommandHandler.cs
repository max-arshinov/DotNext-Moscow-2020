using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class CompleteOrderCommandHandler: ICommandHandler<CompleteOrder, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IHandler<Order.Shipped, Task<Order.Complete>> _handler;
        private readonly IQueryable<Order> _orders;

        public CompleteOrderCommandHandler(IHandler<Order.Shipped, Task<Order.Complete>> handler, 
            IQueryable<Order> orders)
        {
            _handler = handler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(CompleteOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = await order.With((Order.Shipped shippedOrder) => _handler.Handle(shippedOrder));
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}