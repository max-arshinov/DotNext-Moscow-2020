using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class PayOrderCommandHandler : ICommandHandler<PayOrderContext, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueryable<Order> _orders;

        public PayOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IQueryable<Order> orders)
        {
            _unitOfWork = unitOfWork;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(PayOrderContext input)
        {
            await Task.Delay(1000);
            var result = input.Order.With((Order.New newOrder) => newOrder.BecomePaid());
            _unitOfWork.Commit();
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}