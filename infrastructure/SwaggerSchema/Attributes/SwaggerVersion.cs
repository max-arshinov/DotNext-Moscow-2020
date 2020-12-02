using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    public class SwaggerVersion : Attribute
    {
        public string GroupName { get; }

        public SwaggerVersion(string groupName)
        {
            GroupName = groupName;
        }
    }
}