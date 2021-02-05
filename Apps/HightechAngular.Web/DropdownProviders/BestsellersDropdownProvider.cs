using System;
using System.Threading.Tasks;
using HightechAngular.Web.Features.Index;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Web.Features.Index
{
    public class BestsellersDropdownProvider : IDropdownProvider<BestsellersListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public BestsellersDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<BestsellersListItem>();
        }
    }
}