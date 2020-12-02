using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomHandlerAttribute: Attribute
    {
        public string CustomHandlerName { get; }

        public CustomHandlerAttribute(string name)
        {
            CustomHandlerName = name;
        }
    }
}