using System;
using System.Threading.Tasks;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class AllOrdersDropdownProvider : IDropdownProvider<AllOrdersItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public AllOrdersDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<AllOrdersItem>();
        }
    }
}