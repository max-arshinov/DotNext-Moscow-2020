using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomHandlerAttribute : Attribute
    {
        public CustomHandlerAttribute(string name)
        {
            CustomHandlerName = name;
        }

        public string CustomHandlerName { get; }
    }
}