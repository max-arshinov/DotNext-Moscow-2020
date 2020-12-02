using System;
using System.Threading.Tasks;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Shop.Features.Index
{
    public class NewArrivalsDropdownProvider : IDropdownProvider<NewArrivalsListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public NewArrivalsDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<NewArrivalsListItem>();
        }
    }
}