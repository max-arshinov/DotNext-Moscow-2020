using System.ComponentModel.DataAnnotations;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class CompleteOrderByAdmin : ICommand<CommandResult<OrderStatus>>, IHasOrderId
    {
        [Required]
        public int OrderId { set; get; }
    }
}