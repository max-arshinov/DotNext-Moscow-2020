using System;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class PayMyOrder : ICommand<Task<CommandResult<OrderStatus>>>
    {
        public int OrderId { get; set; }
    }
}