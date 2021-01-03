using System;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Orders
{
    public static class CoreRegistrations
    {
        public static void RegisterCore(this IServiceCollection services) { }

        public static IServiceCollection AddOrderStateTransition<TCommand, TFrom, TTo>(
            this IServiceCollection services)
            where TFrom : Order.OrderStateBase
            where TTo : Order.OrderStateBase
            where TCommand : class, ICommand<Task<CommandResult<OrderStatus>>>, IHasOrderId
        {
            services.AddScoped<
                ICommandHandler<ChangeOrderStateConext<TCommand, TFrom>, Task<CommandResult<OrderStatus>>>,
                ChangeOrderStateCommandHandler<TCommand, TFrom, TTo>>();

            var funcType = typeof(Func<TCommand, ChangeOrderStateConext<TCommand, TFrom>>);
            var factoryType = typeof(OperationContextFactory<TCommand, ChangeOrderStateConext<TCommand, TFrom>>);
            
            services.AddScoped(factoryType);
            services.AddScoped(funcType, sp => ((dynamic) sp.GetService(factoryType)).BuildFunc(sp) );

            return services;
        }
    }
}