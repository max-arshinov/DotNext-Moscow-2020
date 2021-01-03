using HightechAngular.Orders;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Admin
{
    public static class AdminRegistrations
    {
        public static void RegisterAdmin(this IServiceCollection services)
        {
            services
                .AddOrderStateTransition<ShipOrder, Order.Paid, Order.Shipped>()
                .AddOrderStateTransition<ResolveDisputedOrder, Order.Disputed, Order.Complete>();
        }
    }
}