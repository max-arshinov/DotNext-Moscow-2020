using System;
using System.Threading.Tasks;
using HightechAngular.Shop.Features.MyOrders;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using JetBrains.Annotations;

namespace HightechAngular.Admin.Features.OrderManagement
{
    [UsedImplicitly]
    public class OrderListItemDropdownProvider : IDropdownProvider<AllOrdersListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderListItemDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<AllOrdersListItem>();
        }
    }
}