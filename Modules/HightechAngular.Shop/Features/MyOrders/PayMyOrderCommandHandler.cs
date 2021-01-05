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
        private readonly IUnitOfWork _unitOfWork;

        public PayMyOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<OrderStatus>> Handle(PayMyOrder input)
        {
            var order = _unitOfWork.Find<Order>(input.OrderId);
            
            if (order?.Status != OrderStatus.New)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            order.BecomePaid();
            await Task.Delay(500); // Third-party API call
            return order.Status;
        }
    }
}