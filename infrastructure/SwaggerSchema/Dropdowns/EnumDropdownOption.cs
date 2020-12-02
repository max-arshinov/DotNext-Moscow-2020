using System;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public class EnumDropdownOption<T>: DropdownOption<string>
        where T: Enum
    {
        internal EnumDropdownOption()
        {
        }
        
        public T EnumValue { get; set; }

        public override string Value
        {
            get => EnumValue.ToString();
            internal set
            {
                
            }
        }
    }
}