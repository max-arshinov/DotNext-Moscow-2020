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
    public class PayOrderCommandHandler : ICommandHandler<PayOrder, Task<CommandResult<OrderStatus>>>
    {
        private readonly IQueryable<Order> _orders;

        public PayOrderCommandHandler(IQueryable<Order> orders)
        {
            _orders = orders;
        }
        
        public async Task<CommandResult<OrderStatus>> Handle(PayOrder input)
        {
            var order = _orders.FirstOrDefault(x => x.Id == input.OrderId);
            
            if (order?.Status != OrderStatus.New)
            {
                return FailureInfo.Invalid("Order doesn't exist or it is in invalid state");
            }
            
            order.BecomePaid();
            await Task.Delay(500); // Third-party API call
            return order.Status;
        }
    }
}