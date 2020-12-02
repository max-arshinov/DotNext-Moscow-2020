using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class DisplayAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "title";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiString(displayAttribute.GetName()));
            }
        }
    }
}