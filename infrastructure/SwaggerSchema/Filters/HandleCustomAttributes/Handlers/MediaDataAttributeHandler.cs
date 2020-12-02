using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class MediaDataAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "mediaPropertyName";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(MediaDataAttribute)) is MediaDataAttribute mediaTypeAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiString(mediaTypeAttribute.MediaTypePropertyName));
            }
        }
    }
}