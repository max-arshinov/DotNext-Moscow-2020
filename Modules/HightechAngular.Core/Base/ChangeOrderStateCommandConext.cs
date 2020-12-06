using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;

namespace HightechAngular.Orders.Base
{
    public class ChangeOrderStateConext<TCommand, TState> :
        OperationContextBase<TCommand>,
        IHasOrderState<TState>,
        ICommand<Task<CommandResult<OrderStatus>>> 
        where TCommand : class, IHasOrderId
        where TState : Order.OrderStateBase
    {
        public ChangeOrderStateConext(TCommand request, Order order) : base(request)
        {
            Order = order;
        }

        [Required]
        public Order Order { get; }

        [Required]
        public TState State => Order?.As<TState>();
    }
}