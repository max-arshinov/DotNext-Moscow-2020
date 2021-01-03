using HightechAngular.Orders;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using HightechAngular.Orders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Shop
{
    public static class ShopRegistrations
    {
        public static void RegisterShop(this IServiceCollection services)
        {
            services.AddScoped<ICartStorage, CartStorage>();

            services
                .AddOrderStateTransition<PayOrder, Order.New, Order.Paid>()
                .AddOrderStateTransition<DisputeOrder, Order.Shipped, Order.Disputed>()
                .AddOrderStateTransition<CompleteOrder, Order.Shipped, Order.Complete>();
        }
    }
}