using System;
using System.Threading.Tasks;
using HightechAngular.Web.Features.Index;
using HightechAngular.Web.Features.Index.GetArrival;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Shop.Features.Index
{
    public class NewArrivalsDropdownProvider : IDropdownProvider<GetNewArrivalsListItem>
    {
        private readonly IServiceProvider _serviceProvider;

        public NewArrivalsDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<GetNewArrivalsListItem>();
        }
    }
}