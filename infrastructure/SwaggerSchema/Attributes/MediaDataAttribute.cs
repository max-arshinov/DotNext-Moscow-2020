using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MediaDataAttribute : Attribute
    {
        public MediaDataAttribute(string mediaTypePropertyName)
        {
            MediaTypePropertyName = mediaTypePropertyName;
        }

        public string MediaTypePropertyName { get; }
    }
}