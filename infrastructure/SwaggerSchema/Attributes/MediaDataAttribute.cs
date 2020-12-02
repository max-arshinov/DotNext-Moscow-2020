using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MediaDataAttribute : Attribute
    {
        public string MediaTypePropertyName { get; }

        public MediaDataAttribute(string mediaTypePropertyName)
        {
            MediaTypePropertyName = mediaTypePropertyName;
        }
    }
}