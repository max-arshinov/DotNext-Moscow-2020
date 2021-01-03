using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Base
{
    public abstract class ChangeOrderStateCommandBase :
        HasIdBase,
        IHasOrderId,
        ICommand<Task<CommandResult<OrderStatus>>>
    {
        [Range(1, int.MaxValue)]
        public int OrderId { get; set; }
    }
}