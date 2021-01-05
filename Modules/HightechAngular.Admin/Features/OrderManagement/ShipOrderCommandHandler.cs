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
    public class ShipOrderCommandHandler : ICommandHandler<ShipOrderContext, Task<CommandResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public ShipOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }

        public async Task<CommandResult<OrderStatus>> Handle(ShipOrderContext input)
        {
            if (input.Order.Status != OrderStatus.Paid)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            input.Order.BecomeShipped(Guid.NewGuid());
            await Task.Delay(500); // Third-party API call
            return input.Order.Status;
        }
    }
}