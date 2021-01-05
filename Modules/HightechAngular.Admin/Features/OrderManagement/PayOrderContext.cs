using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class PayOrderContext: 
        ByIntIdOperationContextBase<PayOrder>, 
        ICommand<Task<CommandResult<OrderStatus>>>
    {
        [Required]
        public Order Order { get; set; }
        
        public PayOrderContext(PayOrder request, Order order) : base(request)
        {
            Order = order;
        }
    }
}