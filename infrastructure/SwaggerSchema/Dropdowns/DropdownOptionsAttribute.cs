using System;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DropdownOptionsAttribute: Attribute
    {
        public Type PropertyProviderType { get; }

        public DropdownOptionsAttribute(Type propertyProviderType)
        {
            PropertyProviderType = propertyProviderType ?? throw new ArgumentNullException(nameof(propertyProviderType));
        }
    }
}