using HightechAngular.Admin.Features.OrderManagement;
using HightechAngular.Orders;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using HightechAngular.Shop.Features.MyOrders;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Admin
{
    public static class AdminRegistrations
    {
        public static void RegisterAdmin(this IServiceCollection services)
        {
            services.AddScoped<IDropdownProvider<PayOrder>, PayOrderDropdownProvider>();
            services.AddScoped<IDropdownProvider<OrderListItem>, OrderListItemDropdownProvider>();
            services.AddScoped<IDropdownProvider<AllOrdersItem>, AllOrdersDropdownProvider>();
            
            services
                .AddOrderStateTransition<ShipOrder, Order.Paid, Order.Shipped>()
                .AddOrderStateTransition<ResolveDisputedOrder, Order.Disputed, Order.Complete>();
        }
    }
}