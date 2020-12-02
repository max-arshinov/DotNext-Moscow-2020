using System;

namespace HightechAngular.SwaggerSchema.Dropdowns
{
    public class DropdownProvider: IDropdownProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DropdownProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public DropdownOptions GetDropdownOptions(Type t)
        {
            // dynamic is to avoid casting to IDropdownProvider<T> where T is unknown in runtime
            return ((dynamic) _serviceProvider
                    .GetService(typeof(IDropdownProvider<>).MakeGenericType(t)))
                ?.GetDropdownOptions();
        }
    }
}