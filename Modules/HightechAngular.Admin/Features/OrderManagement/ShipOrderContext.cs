using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ShipOrderContext: 
        ByIntIdOperationContextBase<ShipOrder>, 
        ICommand<Task<CommandResult<OrderStatus>>>
    {
        [Required]
        public Order Order { get; set; }
        
        public ShipOrderContext(ShipOrder request, Order order) : base(request)
        {
            Order = order;
        }
    }
}