using System.Data.Common;
using System.Threading.Tasks;
using Force.Ccc;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace HightechAngular.Orders.Handlers
{
    [UsedImplicitly]
    public class DisputeOrderHandler :
        ChangeOrderStateHandlerBase<DisputeOrder, Order.Shipped, Order.Disputed>
    {
        public DisputeOrderHandler(IUnitOfWork unitOfWork, ILogger<DisputeOrder> logger) : base(unitOfWork, logger)
        {
        }


        protected override Order.Disputed ChangeState(ChangeOrderStateConext<DisputeOrder, Order.Shipped> input)
        {
            return input.State.BecomeDisputed(input.Request.Complaint);
        }

        protected async override Task ChangeStateInRemoteSystem(
            ChangeOrderStateConext<DisputeOrder, Order.Shipped> input)
        {
            await Task.Delay(300); // Imitate API Request
        }

        protected async override Task RollbackRemoteSystem(ChangeOrderStateConext<DisputeOrder, Order.Shipped> input,
            DbException e)
        {
            await Task.Delay(300); // Imitate API Request
        }
    }
}