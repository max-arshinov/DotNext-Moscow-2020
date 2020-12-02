using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public class Dropdown
    {
        private readonly object _data;

        internal Dropdown(IEnumerable<DropdownOption> options, string name, bool isMulti = true, object data = null)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            _data = data;
            IsMulti = isMulti;
            Name = name;
        }
        
        public bool IsMulti { get; }
        public string Name { get; }
        public bool Selected { get; internal set; }
        public int Order { get; set; }

        public IEnumerable<DropdownOption> Options { get; internal set; }

        public Dropdown Prepend(DropdownOption option)
        {
            Options = Options.Prepend(option);
            return this;
        }
    }
}