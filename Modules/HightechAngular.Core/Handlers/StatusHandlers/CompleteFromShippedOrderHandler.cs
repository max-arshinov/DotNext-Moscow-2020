using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Handlers
{
    public class CompleteFromShippedOrderHandler: IHandler<Order.Shipped, Task<Order.Complete>>
    {
        public async  Task<Order.Complete> Handle(Order.Shipped input)
        {
            await Task.Delay(1000);
            var order = input.BecomeComplete();
            return order;
        }
    }
}