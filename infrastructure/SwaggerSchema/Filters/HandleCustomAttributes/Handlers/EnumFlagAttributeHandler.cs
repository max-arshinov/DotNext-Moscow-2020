using System;
using System.Reflection;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class EnumFlagAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "isMultiSelect";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(FlagsAttribute)) != null && property.PropertyType.IsEnum ||
                property.PropertyType.IsArray)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiBoolean(true));
            }
        }
    }
}