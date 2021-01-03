using System;
using System.Threading.Tasks;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class MyOrdersListItemDropdownProvider : IDropdownProvider<MyOrdersListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public MyOrdersListItemDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<MyOrdersListItem>();
        }
    }
}