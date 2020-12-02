using System;
using System.Threading.Tasks;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class CreateOrderDropdownProvider : IDropdownProvider<AllOrdersItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateOrderDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<AllOrdersItem>();
        }
    }
}