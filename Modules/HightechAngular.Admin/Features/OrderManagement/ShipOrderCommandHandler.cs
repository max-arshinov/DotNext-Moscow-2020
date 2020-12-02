using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrder, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public ShipOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(ShipOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            await Task.Delay(1000);
            var result = order.BecomeShipped();
            return new HandlerResult<OrderStatus>(result);
        }
    }
}