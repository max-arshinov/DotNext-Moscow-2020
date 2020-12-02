using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Handlers
{
    public class CompleteFromDisputeOrderHandler: IHandler<Order.Dispute, Task<Order.Complete>>
    {
        public async Task<Order.Complete> Handle(Order.Dispute input)
        {
            await Task.Delay(1000);
            var order = input.BecomeComplete();
            return order;
        }
    }
}