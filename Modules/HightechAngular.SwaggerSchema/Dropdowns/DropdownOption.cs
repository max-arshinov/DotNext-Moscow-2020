using System;

namespace HightechAngular.SwaggerSchema.Dropdowns
{
    public class DropdownOption
    {
        public DropdownOption(object value, object label)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public object Value { get; }
        
        public object Label { get; }
    }
}