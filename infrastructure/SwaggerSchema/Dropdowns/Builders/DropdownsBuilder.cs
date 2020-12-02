using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    public class DropdownsBuilder<T>
    {
        protected readonly Dictionary<string, Dropdown> Options = 
            new Dictionary<string, Dropdown>();
        
        public static implicit operator Task<Dropdowns>(DropdownsBuilder<T> builder) => 
            builder.BuildAsync();

        internal DropdownsBuilder()
        {
        }
        
        public virtual Task<Dropdowns> BuildAsync() =>
            Task.FromResult(new Dropdowns(Options));

        public DropdownsBuilder<T> With<TProperty>(Expression<Func<T, TProperty>> expression, 
            IEnumerable<DropdownOption<TProperty>> options)
        {
            var memberName = GetMemberParams(expression);
            Options[memberName.Key] = new Dropdown(options.ToList(), memberName.Name);
            return this;
        }

        protected static string GetMemberName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var me = expression.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("Expression is not a member expression", nameof(expression));
            }
            
            var memberName = me.Member.IsDefined(typeof(DisplayAttribute), false) ?
                me.Member.GetCustomAttribute<DisplayAttribute>().Name :
                me.Member.Name;
            return  me.Member.Name;
        }

        protected static MemberParams GetMemberParams<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var me = expression.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("Expression is not a member expression", nameof(expression));
            }
            
            var memberName = me.Member.IsDefined(typeof(DisplayAttribute), false) ?
                me.Member.GetCustomAttribute<DisplayAttribute>().Name :
                me.Member.Name;
            return  new MemberParams() {Name = memberName, Key = me.Member.Name};
        }
        protected class MemberParams
        {
            public string Name { get; set; }
            public string Key { get; set; }
        }
    }
}