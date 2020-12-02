using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class CompleteOrderAdminCommandHandler: ICommandHandler<CompleteOrderAdmin, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IHandler<Order.Dispute, Task<Order.Complete>> _handler;
        private readonly IQueryable<Order> _orders;

        public CompleteOrderAdminCommandHandler(IHandler<Order.Dispute, Task<Order.Complete>> handler, 
            IQueryable<Order> orders)
        {
            _handler = handler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(CompleteOrderAdmin input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = await order.With((Order.Dispute disputeOrder) => _handler.Handle(disputeOrder));
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}