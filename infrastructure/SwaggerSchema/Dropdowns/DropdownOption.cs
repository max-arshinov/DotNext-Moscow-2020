using System;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public class DropdownOption
    {
        internal DropdownOption() { }

        internal DropdownOption(string label, object value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }


        public string Label { get; internal set; }

        public object Value { get; internal set; }

        public static DropdownOption<T> Create<T>(string label, T value)
        {
            return new DropdownOption<T>(label, value);
        }

        public static DropdownOption<T> Create<T>(string label, T value, int count)
        {
            return new DropdownOption<T>(label, value, count);
        }

        public static DropdownOption<string> Create(string label, string value)
        {
            return new DropdownOption<string>(label, value);
        }

        public static DropdownOption<string> Create(string label, object value)
        {
            return new DropdownOption<string>(label, value.ToString());
        }
    }

    public class DropdownOption<T> : DropdownOption
    {
        private T _value;

        internal DropdownOption() { }

        public DropdownOption(string label, T value) : base(label, value)
        {
            _value = value;
            base.Value = value;
        }

        public DropdownOption(string label, T value, int count) : base(label, value)
        {
            _value = value;
            base.Value = value;
        }

        public new virtual T Value
        {
            get => (T) base.Value;
            internal set => base.Value = value;
        }
    }
}