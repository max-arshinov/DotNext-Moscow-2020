using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    public class SwaggerHeaderAttribute : Attribute
    {
        public string HeaderName { get; }
        public string Description { get; }
        public string DefaultValue { get; }
        public bool IsRequired { get; }

        public SwaggerHeaderAttribute(string headerName, string description = null, string defaultValue = null, bool isRequired = false)
        {
            HeaderName = headerName;
            Description = description;
            DefaultValue = defaultValue;
            IsRequired = isRequired;
        }
    }
}