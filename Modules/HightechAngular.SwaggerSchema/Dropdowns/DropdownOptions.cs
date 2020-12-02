using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HightechAngular.SwaggerSchema.Dropdowns
{
    public class DropdownOptions : Dictionary<string, IEnumerable<DropdownOption>>
    {
    }

    public class DropdownOptions<T> : DropdownOptions
    {
        public IEnumerable<DropdownOption> this[Expression<Func<T, object>> expression]
        {
            get
            {
                if (expression == null) throw new ArgumentNullException(nameof(expression));
                var me = expression.Body as MemberExpression;
                if (me == null)
                {
                    if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                    {
                        me = operand;
                    }
                    else
                    {
                        throw new ArgumentException("Expression is not a member expression", nameof(expression));
                    }
                }

                return this[me.Member.Name];
            }
            set
            {
                if (expression == null) throw new ArgumentNullException(nameof(expression));
                var me = expression.Body as MemberExpression;
                if (me == null)
                {
                    if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                    {
                        me = operand;
                    }
                    else
                    {
                        throw new ArgumentException("Expression is not a member expression", nameof(expression));
                    }
                }

                this[me.Member.Name] = value;
            }
        }
    }
}