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
        private readonly IHandler<Order.New, Task<Order.Paid>> _payOrderHandler;
        private readonly IQueryable<Order> _orders;

        public PayOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IHandler<Order.New, Task<Order.Paid>> payOrderHandler, 
            IQueryable<Order> orders)
        {
            _unitOfWork = unitOfWork;
            _payOrderHandler = payOrderHandler;
            _orders = orders;
        }

        public async Task<HandlerResult<OrderStatus>> Handle(PayOrderContext input)
        {
            var result = await input.Order.With((Order.New newOrder) => _payOrderHandler.Handle(newOrder));
            _unitOfWork.Commit();
            return new HandlerResult<OrderStatus>(result.EligibleStatus);
        }
    }
}