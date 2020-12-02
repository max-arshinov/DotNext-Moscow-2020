using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class BoolInlineEditingAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "boolInlineEditing";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(BoolInlineEditingAttribute)) is
                BoolInlineEditingAttribute boolInlineEditingAttribute)
            {
                var openApiObject = new OpenApiObject();
                openApiObject["boolInlineEditingChangePath"] =
                    new OpenApiString(boolInlineEditingAttribute.BoolInlineEditingChangePath);
                openApiObject["boolInlineDeletingIdQueryParamName"] =
                    new OpenApiString(boolInlineEditingAttribute.BoolInlineDeletingIdQueryParamName);
                openApiObject["boolInlineDeletingIdPropertyName"] =
                    new OpenApiString(boolInlineEditingAttribute.BoolInlineDeletingIdPropertyName);
                Properties.Add(
                    property.Name.ToLower(),
                    openApiObject);
            }
        }
    }
}