using System;
using System.Threading.Tasks;
using HightechAngular.Web.Dto.OrderManagement;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;

namespace HightechAngular.Web.Features.OrderManagement
{
    public class PayOrderDropdownProvider : IDropdownProvider<PayOrder>
    {
        private readonly IServiceProvider _serviceProvider;

        public PayOrderDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<PayOrder>();
        }
    }
}