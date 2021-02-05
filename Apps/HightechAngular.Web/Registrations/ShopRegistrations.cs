using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using HightechAngular.Shop.Features.Cart;
using HightechAngular.Shop.Features.Catalog;
using HightechAngular.Shop.Features.Index;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Shop
{
    public static class ShopRegistrations
    {
        public static void RegisterShop(this IServiceCollection services)
        {
            services.AddScoped<ICartStorage, CartStorage>();
            services.AddScoped<IDropdownProvider<ProductListItem>, ProductsDropdownProvider>();
            services.AddScoped<IDropdownProvider<BestsellersListItem>, BestsellersDropdownProvider>();
            services.AddScoped<IDropdownProvider<NewArrivalsListItem>, NewArrivalsDropdownProvider>();
            services.AddScoped<IDropdownProvider<SaleListItem>, SaleListDropdownProvider>();
            services.AddScoped<IDropdownProvider<CartItem>, CartDropdownProvider>();
        }
    }
}