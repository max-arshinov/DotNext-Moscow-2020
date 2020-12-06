using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Base
{
    public class ChangeOrderStateCommandHandler<TCommand, TFrom, TTo> :
        ICommandHandler<
            ChangeOrderStateConext<TCommand, TFrom>, 
            Task<CommandResult<OrderStatus>>>
    
        where TCommand : class, ICommand<Task<CommandResult<OrderStatus>>>, IHasOrderId
        where TFrom : Order.OrderStateBase
        where TTo : Order.OrderStateBase
    {
        private readonly IHandler<
            ChangeOrderStateConext<TCommand, TFrom>, 
            Task<CommandResult<TTo>>> _handler;

        public ChangeOrderStateCommandHandler(
            IHandler<
                ChangeOrderStateConext<TCommand, TFrom>, 
                Task<CommandResult<TTo>>> handler)
        {
            _handler = handler;
        }
        
        public Task<CommandResult<OrderStatus>> Handle(ChangeOrderStateConext<TCommand, TFrom> input) =>
            _handler
                .Handle(input)
                .MapAsync(x => x.EligibleStatus);
    }
}