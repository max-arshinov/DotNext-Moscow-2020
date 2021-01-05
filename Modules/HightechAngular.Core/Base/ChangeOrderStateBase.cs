using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Base
{
    public abstract class ChangeOrderStateBase: 
        ICommand<Task<CommandResult<OrderStatus>>>, 
        IHasId<int>
    {
        [Required]
        public int OrderId { get; set; }

        object IHasId.Id => Id;

        public int Id => OrderId;
    }
}