using System;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Workflow;

namespace HightechAngular.Orders.Handlers
{
    public class PayOrderHandler: IHandler<Order.New, Task<Order.Paid>>
    {
        public async Task<Order.Paid> Handle(Order.New input)
        {
            await Task.Delay(1000);
            var order = input.BecomePaid();
            return order;
        }
    }
}