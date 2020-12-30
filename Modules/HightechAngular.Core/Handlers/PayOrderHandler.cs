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
    public class PayOrderHandler : ChangeOrderStateHandlerBase<
        PayOrder,
        Order.New,
        Order.Paid>
    {
        public PayOrderHandler(IUnitOfWork unitOfWork, ILogger<PayOrder> logger) : base(unitOfWork, logger) { }

        protected override Order.Paid ChangeState(ChangeOrderStateConext<PayOrder, Order.New> input)
        {
            return input.State.BecomePaid();
        }

        protected override async Task ChangeStateInRemoteSystem(ChangeOrderStateConext<PayOrder, Order.New> input)
        {
            await Task.Delay(300); // Imitate API Request
        }

        protected override async Task RollbackRemoteSystem(ChangeOrderStateConext<PayOrder, Order.New> input,
            DbException e)
        {
            await Task.Delay(300); // Imitate API Request
        }
    }
}