using System;
using System.Threading.Tasks;
using HightechAngular.Orders.Entities;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Shop.Features.Cart
{
    public class CartDropdownProvider : IDropdownProvider<CartItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public CartDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<CartItem>();
        }
    }
}