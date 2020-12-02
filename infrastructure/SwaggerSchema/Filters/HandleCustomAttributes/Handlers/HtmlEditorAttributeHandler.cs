using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class HtmlEditorAttributeHandler: AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "htmlEditor";
        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(HtmlEditorAttribute)) is HtmlEditorAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiBoolean(true));
            }
        }
    }
}