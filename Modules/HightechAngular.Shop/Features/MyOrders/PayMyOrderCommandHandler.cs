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
        private readonly IHandler<Order.New, Task<Order.Paid>> _payOrderHandler;
        private readonly IQueryable<Order> _orders;

        public PayMyOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IHandler<Order.New, Task<Order.Paid>> payOrderHandler, 
            IQueryable<Order> orders)
        {
            _unitOfWork = unitOfWork;
            _payOrderHandler = payOrderHandler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(PayMyOrder input)
        {
            var order = _orders.First(x => x.Id == input.OrderId);
            var result = await order.With((Order.New newOrder) => _payOrderHandler.Handle(newOrder));
            _unitOfWork.Commit();
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}