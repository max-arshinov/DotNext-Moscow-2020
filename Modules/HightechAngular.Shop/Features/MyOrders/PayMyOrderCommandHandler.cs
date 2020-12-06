using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class PayMyOrderCommandHandler : ICommandHandler<PayMyOrder, Task<CommandResult<OrderStatus>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHandler<Order.New, Task<CommandResult<Order.Paid>>> _payOrderHandler;
        private readonly IQueryable<Order> _orders;

        public PayMyOrderCommandHandler(
            IUnitOfWork unitOfWork,
            //IHandler<Order.New, Task<CommandResult<Order.Paid>>> payOrderHandler, 
            IQueryable<Order> orders)
        {
            _unitOfWork = unitOfWork;
            //_payOrderHandler = payOrderHandler;
            _orders = orders;
        }

        public async Task<CommandResult<OrderStatus>> Handle(PayMyOrder input)
        {
            throw new NotImplementedException();
            // var order = _orders.First(x => x.Id == input.OrderId);
            // var result = await order.With((Order.New newOrder) => _payOrderHandler.Handle(newOrder));
            // _unitOfWork.Commit();
            // return result.Map(x => x.EligibleStatus);
        }
    }
}