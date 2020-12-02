using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public class DropdownOptions : Dictionary<string, IEnumerable<DropdownOption>>
    {
    }

    public class DropdownOptions<T> : DropdownOptions
    {
        public DropdownOptions<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, 
            IEnumerable<DropdownOption<TProperty>> options)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var me = expression.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("Expression is not a member expression", nameof(expression));
            }

            this[me.Member.Name] = options;
            return this;
        }
    }
}