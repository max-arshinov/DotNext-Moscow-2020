using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal abstract class DropdownBuilder : IDropdownBuilder
    {
        public abstract Task<Dropdown> BuildAsync();
    }
}