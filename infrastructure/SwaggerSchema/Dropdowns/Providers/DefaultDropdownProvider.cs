using System;
using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Providers
{
    public class DefaultDropdownProvider: IDropdownProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultDropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task<Dropdowns> GetDropdownOptionsAsync(Type t)
        {
            return ((dynamic) _serviceProvider
                    .GetService(typeof(IDropdownProvider<>).MakeGenericType(t)))
                ?.GetDropdownOptionsAsync();
        }
    }
}