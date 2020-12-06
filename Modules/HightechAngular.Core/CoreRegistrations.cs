using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Orders
{
    public static class CoreRegistrations
    {
        public static IServiceCollection AddOrderStateTransition<TCommand, TFrom, TTo>(
            this IServiceCollection services)
            where TFrom : Order.OrderStateBase
            where TTo : Order.OrderStateBase
            where TCommand : class, ICommand<Task<CommandResult<OrderStatus>>>, IHasOrderId
        {
            return services.AddScoped<
                ICommandHandler<ChangeOrderStateConext<TCommand, TFrom>, Task<CommandResult<OrderStatus>>>,
                ChangeOrderStateCommandHandler<TCommand, TFrom, TTo>>();
        }
    }
}