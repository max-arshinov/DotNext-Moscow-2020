using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class PayMyOrderCommandHandler : ICommandHandler<PayMyOrder, Task<HandlerResult<OrderStatus>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueryable<Order> _orders;

        public PayMyOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IQueryable<Order> orders)
        {
            _unitOfWork = unitOfWork;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(PayMyOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            await Task.Delay(1000);
            var result = order.BecomePaid();
            _unitOfWork.Commit();
            return new HandlerResult<OrderStatus>(result);
        }
    }
}