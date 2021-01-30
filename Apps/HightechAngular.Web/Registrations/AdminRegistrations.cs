using HightechAngular.Admin.Features.OrderManagement;
using HightechAngular.Web.Dto.OrderManagement;
using HightechAngular.Web.Features.MyOrders;
using HightechAngular.Web.Features.OrderManagement;
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
            services.AddScoped<IDropdownProvider<AllOrdersItem>, CreateOrderDropdownProvider>();
        }
    }
}