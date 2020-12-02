using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal interface IDropdownBuilder
    {
        Task<Dropdown> BuildAsync();
    }
}