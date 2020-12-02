using System;
using System.Collections.Generic;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TextHighlighterAttribute : Attribute
    {
        public string Value { get; set; }
        public string Color { get; set; }
    }
}