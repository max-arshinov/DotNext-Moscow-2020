using Force.Ccc;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace HightechAngular.Orders.Handlers
{
    [UsedImplicitly]
    public class CompletedOrderHandler : CompleteOrderHandlerBase<CompleteOrder, Order.Shipped, Order.Complete>
    {
        public CompletedOrderHandler(IUnitOfWork unitOfWork, ILogger<CompleteOrder> logger) :
            base(unitOfWork, logger) { }
        
        protected override Order.Complete ChangeState(ChangeOrderStateConext<CompleteOrder, Order.Shipped> input)
        {
            return input.State.BecomeComplete();
        }
    }
}