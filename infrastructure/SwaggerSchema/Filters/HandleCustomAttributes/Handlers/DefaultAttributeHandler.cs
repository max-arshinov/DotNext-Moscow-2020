using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class DefaultAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "isDefault";
        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(DefaultAttribute)) is DefaultAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiBoolean(true));
            }
        }
    }
}