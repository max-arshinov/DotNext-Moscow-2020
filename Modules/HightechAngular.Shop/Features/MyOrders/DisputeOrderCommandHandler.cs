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
    public class DisputeOrderCommandHandler : ICommandHandler<DisputeOrder, Task<CommandResult<OrderStatus>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisputeOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<CommandResult<OrderStatus>> Handle(DisputeOrder input)
        {
            var order = _unitOfWork.Find<Order>(input.OrderId);
            
            if (order?.Status != OrderStatus.Shipped)
            {
                return FailureInfo.Invalid("Order is in invalid state");
            }
            
            order.BecomeDisputed(input.Complaint);
            await Task.Delay(500); // Third-party API call
            return order.Status;
        }
    }
}