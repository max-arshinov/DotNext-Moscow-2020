using System;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Web.Features.Admin
{
    public class PayOrder : HasIdBase, ICommand<Task<HandlerResult<OrderStatus>>>
    {
        public int OrderId { get; set; }
    }
}