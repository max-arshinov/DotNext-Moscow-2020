using System;
using System.Threading.Tasks;
using HightechAngular.Web.Features.Account;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class OrderListItemDropdownProvider : IDropdownProvider<OrderListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderListItemDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<OrderListItem>();
        }
    }
}