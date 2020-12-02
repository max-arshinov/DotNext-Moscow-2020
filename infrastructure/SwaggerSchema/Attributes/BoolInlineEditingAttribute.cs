using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BoolInlineEditingAttribute : Attribute
    {
        public string BoolInlineEditingChangePath { get; set; }
        public string BoolInlineDeletingIdQueryParamName { get; set; }
        public string BoolInlineDeletingIdPropertyName { get; set; }
    }
}