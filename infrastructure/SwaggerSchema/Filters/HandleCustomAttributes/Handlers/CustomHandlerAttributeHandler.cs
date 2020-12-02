using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class CustomHandlerAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "customHandler";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(CustomHandlerAttribute)) is
                CustomHandlerAttribute customHandlerAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiString(customHandlerAttribute.CustomHandlerName));
            }
        }
    }
}