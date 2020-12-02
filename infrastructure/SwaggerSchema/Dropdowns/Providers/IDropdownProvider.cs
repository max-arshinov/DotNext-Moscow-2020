using System;
using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Providers
{
    public interface IDropdownProvider
    {
        Task<Dropdowns> GetDropdownOptionsAsync(Type t);
    }

    public interface IDropdownProvider<T>
    {
        Task<Dropdowns> GetDropdownOptionsAsync();
    }
}