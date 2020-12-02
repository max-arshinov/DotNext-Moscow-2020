using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Handlers
{
    public class DisputeOrderHandler: IHandler<Order.Shipped, Task<Order.Dispute>>
    {
        public async Task<Order.Dispute> Handle(Order.Shipped input)
        {
            await Task.Delay(1000);
            var order = input.BecomeDispute();
            return order;
        }
    }
}