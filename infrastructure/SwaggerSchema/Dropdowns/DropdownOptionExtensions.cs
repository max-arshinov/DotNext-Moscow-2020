using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public static class DropdownOptionExtensions
    {
        private static class Cache<T, TDropdownOption>
            where TDropdownOption: DropdownOption<T>
        {
            public static readonly PropertyInfo LabelPropertyInfo = typeof(TDropdownOption)
                .GetProperty("Label");

            public static readonly PropertyInfo ValuePropertyInfo = typeof(TDropdownOption)
                .GetProperties()
                .First(x => x.DeclaringType != typeof(DropdownOption) 
                            && x.CanWrite
                            && x.Name == "Value" 
                            && x.PropertyType == typeof(T));

            public static readonly PropertyInfo CountPropertyInfo = typeof(TDropdownOption)
                .GetProperty("Count");

            public static readonly ConstructorInfo Constructor = typeof(TDropdownOption)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                    null, Type.EmptyTypes, null);

            public static readonly MethodInfo get_Item = typeof(Dictionary<T, int>)
                .GetMethods()
                .Single(x => x.Name == "get_Item");
            
            public static readonly MethodInfo Create = typeof(DropdownOption).GetMethods()
                .Single(x =>
                    x.GetParameters().Length == 3 
                    && x.Name == nameof(DropdownOption.Create) 
                    && x.IsGenericMethod 
                    && x.IsStatic);
        }

        public static IQueryable<DropdownOption<TValue>> ToDropdownOption<TQueryable, TValue, TDropdownOption>(
            this IQueryable<TQueryable> q,
            Expression<Func<TQueryable, string>> labelExpression,
            Expression<Func<TQueryable, TValue>> valueExpression)
            where TDropdownOption: DropdownOption<TValue>
        {
            var newExpression = Expression.New(Cache<TValue, TDropdownOption>.Constructor);
            
            var e2Rebind = Rebind(valueExpression, labelExpression);
            var e1ExpressionBind = Expression.Bind(Cache<TValue, TDropdownOption>.LabelPropertyInfo, labelExpression.Body);
            var e2ExpressionBind = Expression.Bind(Cache<TValue, TDropdownOption>.ValuePropertyInfo, e2Rebind.Body);

            var result = Expression.MemberInit(newExpression, e1ExpressionBind, e2ExpressionBind);
            var lambda = Expression.Lambda<Func<TQueryable, DropdownOption<TValue>>>(result, labelExpression.Parameters);

            return q.Select(lambda);
        }

        public static IQueryable<DropdownOption<string>> ToEnumDropdownOption<TQueryable, TValue>(
            this IQueryable<TQueryable> q,
            Expression<Func<TQueryable, string>> labelExpression,
            Expression<Func<TQueryable, TValue>> valueExpression)
            where TValue: Enum
        {
            var newExpression = Expression.New(Cache<string, EnumDropdownOption<TValue>>.Constructor);
            
            var e2Rebind = Rebind(valueExpression, labelExpression);
            var e1ExpressionBind = Expression.Bind(Cache<string, EnumDropdownOption<TValue>>.LabelPropertyInfo, labelExpression.Body);
            
            var valuePropertyInfo = typeof(EnumDropdownOption<TValue>)
                .GetProperties()
                .First(x => x.DeclaringType != typeof(DropdownOption) 
                            && x.CanWrite
                            && x.Name == "EnumValue" 
                            && x.PropertyType == typeof(TValue));
            
            var e2ExpressionBind = Expression.Bind(valuePropertyInfo, e2Rebind.Body);

            var result = Expression.MemberInit(newExpression, e1ExpressionBind, e2ExpressionBind);
            var lambda = Expression.Lambda<Func<TQueryable, DropdownOption<string>>>(result, labelExpression.Parameters);

            return q.Select(lambda);
        }

        public static IQueryable<DropdownOption<TValue>> ToDropdownOption<TQueryable, TValue>(
            this IQueryable<TQueryable> q, 
            Expression<Func<TQueryable, string>> labelExpression,
            Expression<Func<TQueryable, TValue>> valueExpression)
        {
            return ToDropdownOption<TQueryable, TValue, DropdownOption<TValue>>(q, labelExpression, valueExpression);
        }
        
        public static LambdaExpression Rebind(LambdaExpression replaceable,
            LambdaExpression whichToReplaceable)
        {
            return PredicateBuilder.ParameterRebinder.ReplaceParameters(replaceable.Parameters
                .Zip(whichToReplaceable.Parameters, (x, y) => new {replaceable = x, whichToReplaceable = y})
                .ToDictionary(x => x.replaceable, x => x.whichToReplaceable), replaceable) as LambdaExpression;
        }
    }
}