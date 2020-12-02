using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal class EnumDropdownBuilder<T> : DropdownBuilder
    {

        private readonly Func<IEnumerable<DropdownOption>, IEnumerable<DropdownOption>> _filter;
        private readonly string _name;

        internal EnumDropdownBuilder(Func<IEnumerable<DropdownOption>, IEnumerable<DropdownOption>> filter, string name)
        {
            _filter = filter;
            _name = name;
        }

        public override Task<Dropdown> BuildAsync()
        {
            var type = typeof(T);
            if (type.IsArray)
                type = typeof(T).GetElementType();
            // ReSharper disable once PossibleNullReferenceException
            var res = type
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select((x, i) => new DropdownOption(x.IsDefined(typeof(DisplayAttribute), false)
                    ? x.GetCustomAttribute<DisplayAttribute>().Name
                    : SplitMySpaces(x.Name), x.GetValue(null)));
            
            var array = (_filter == null? res : _filter(res)).ToArray();
            return Task.FromResult(new Dropdown(array, _name));
        }

        private string SplitMySpaces(string str) =>
            Regex.Replace(str, "([A-Z]|[0-9])", " $1", RegexOptions.Compiled);
    }
}