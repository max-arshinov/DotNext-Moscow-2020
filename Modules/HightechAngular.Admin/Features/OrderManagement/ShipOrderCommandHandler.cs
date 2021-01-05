using System;
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
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrder, Task<CommandResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public ShipOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }

        public async Task<CommandResult<OrderStatus>> Handle(ShipOrder input)
        {
            var order = _orders.FirstOrDefault(x => x.Id == input.OrderId);
            
            if (order?.Status != OrderStatus.Paid)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            order.BecomeShipped(Guid.NewGuid());
            await Task.Delay(500); // Third-party API call
            return order.Status;
        }
    }
}