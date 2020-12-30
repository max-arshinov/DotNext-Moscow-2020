using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using Infrastructure.Cqrs;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class PayOrderCommandHandler :
        ICommandHandler<ChangeOrderStateConext<PayOrder, Order.New>, Task<CommandResult<OrderStatus>>>
    {
        private readonly IHandler<
            ChangeOrderStateConext<PayOrder, Order.New>,
            Task<CommandResult<Order.Paid>>> _payOrderHandler;

        public PayOrderCommandHandler(
            IHandler<ChangeOrderStateConext<PayOrder, Order.New>, Task<CommandResult<Order.Paid>>>
                payOrderHandler)
        {
            _payOrderHandler = payOrderHandler;
        }


        public Task<CommandResult<OrderStatus>> Handle(ChangeOrderStateConext<PayOrder, Order.New> input)
        {
            return _payOrderHandler
                .Handle(input)
                .MapAsync(x => x.EligibleStatus);
        }
    }
}