using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Handlers
{
    public class ShipOrderHandler : IHandler<Order.Paid, Task<Order.Shipped>>
    {
        public async Task<Order.Shipped> Handle(Order.Paid input)
        {
            await Task.Delay(1000);
            var order = input.BecomeShipped();
            return order;
        }
    }
}