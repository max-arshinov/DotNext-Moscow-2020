using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class PayOrderCommandHandler : ICommandHandler<PayOrder, Task<HandlerResult<OrderStatus>>>
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

        public async Task<HandlerResult<OrderStatus>> Handle(PayOrder input)
        {
            await Task.Delay(1000);
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = order.BecomePaid();
            _unitOfWork.Commit();
            return new HandlerResult<OrderStatus>(result);
        }
    }
}