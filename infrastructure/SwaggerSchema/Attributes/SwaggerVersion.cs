using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    public class SwaggerVersion : Attribute
    {
        public SwaggerVersion(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; }
    }
}