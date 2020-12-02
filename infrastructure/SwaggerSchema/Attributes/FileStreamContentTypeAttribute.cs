using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FileStreamContentTypeAttribute: Attribute
    {
        public FileStreamContentTypeAttribute(string[] contentTypes)
        {
            ContentTypes = contentTypes;
        }

        public string[] ContentTypes { get; }
    }
}