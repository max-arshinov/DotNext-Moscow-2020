using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class HiddenAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "isHidden";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(HiddenInputAttribute)) is HiddenInputAttribute
                hiddenInputAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiBoolean(hiddenInputAttribute.DisplayValue));
            }
        }
    }
}