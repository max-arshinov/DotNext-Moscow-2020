using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisableFormControlIfTargetPropertyIsNotNullAttribute : Attribute
    {
        public string TargetPropertyName { get; }
        public DisableFormControlIfTargetPropertyIsNotNullAttribute(string targetPropertyName)
        {
            TargetPropertyName = targetPropertyName;
        }
    }
}