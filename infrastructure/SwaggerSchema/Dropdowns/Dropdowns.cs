using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infrastructure.SwaggerSchema.Dropdowns.Builders;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public class Dropdowns : ReadOnlyDictionary<string, Dropdown>
    {
        internal Dropdowns(IDictionary<string, Dropdown> dictionary) : base(dictionary)
        {
        }
        
        public static DropdownsBuilder<T> Create<T>()
        {
            return new DropdownsBuilder<T>();
        }
        
        public static ParallelDropdownsBuilder<T> Create<T>(IServiceProvider serviceProvider) 
            where T : class
        {
            return new ParallelDropdownsBuilder<T>(serviceProvider);
        }
    }
}