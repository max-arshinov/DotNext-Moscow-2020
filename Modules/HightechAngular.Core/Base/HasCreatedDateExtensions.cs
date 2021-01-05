using System;
using System.Linq;
using System.Linq.Expressions;
using Force.Cqrs;
using Force.Reflection;

namespace HightechAngular.Orders.Base
{
    public class HasCreatedFilterQuery<T>: FilterQuery<T>
        where T: IHasCreatedDateString
    {
        private static readonly Expression<Func<T, DateTime>> CreatedExpression =
            Type<T>.PropertyGetter<DateTime>(nameof(IHasCreatedDateString.Created));

        public override IOrderedQueryable<T> Sort(IQueryable<T> queryable)
        {
            if (string.Equals(Order, nameof(IHasCreatedDateString.CreatedString), 
                StringComparison.InvariantCultureIgnoreCase))
            {
                return Asc 
                    ? queryable.OrderByDescending(CreatedExpression) 
                    : queryable.OrderBy(CreatedExpression);
            }
            
            return base.Sort(queryable);
        }
    }
}