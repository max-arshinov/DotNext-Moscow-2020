using System.Collections.Generic;
using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class TextHighlighterAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "textHighlighter";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttributes(typeof(TextHighlighterAttribute)) is
                IEnumerable<TextHighlighterAttribute> textHighlighterAttributes)
            {
                var openApiObject = new OpenApiObject();

                foreach (var textHighlighterAttribute in textHighlighterAttributes)
                {
                    openApiObject[textHighlighterAttribute.Value] = new OpenApiString(textHighlighterAttribute.Color);
                }

                Properties.Add(
                    property.Name.ToLower(),
                    openApiObject);
            }
        }
    }
}