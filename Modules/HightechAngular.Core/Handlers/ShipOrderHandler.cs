using System.Data.Common;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace HightechAngular.Orders.Handlers
{
    [UsedImplicitly]
    public class ShipOrderHandler : ChangeOrderStateHandlerBase<
        ShipOrder, 
        Order.Paid,
        Order.Shipped>
    {
        public ShipOrderHandler(IUnitOfWork unitOfWork, ILogger<ShipOrder> logger) : base(unitOfWork, logger)
        {
        }

        protected override Order.Shipped ChangeState(ChangeOrderStateConext<ShipOrder, Order.Paid> input)
        {
            return input.State.BecomeShipped();
        }

        protected override async Task ChangeStateInRemoteSystem(ChangeOrderStateConext<ShipOrder, Order.Paid> input)
        {
            await Task.Delay(300); // Imitate API Request
        }

        protected override async Task RollbackRemoteSystem(ChangeOrderStateConext<ShipOrder, Order.Paid> input, DbException e)
        {
            await Task.Delay(300); // Imitate API Request
        }
    }
}