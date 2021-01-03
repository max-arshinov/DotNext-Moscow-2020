using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisableFormControlIfTargetPropertyIsNotNullAttribute : Attribute
    {
        public DisableFormControlIfTargetPropertyIsNotNullAttribute(string targetPropertyName)
        {
            TargetPropertyName = targetPropertyName;
        }

        public string TargetPropertyName { get; }
    }
}