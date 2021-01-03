using System;
using System.Linq;
using Force.Cqrs;

namespace HightechAngular.Shop.Features.Index
{
    public class ProductListItemQueryBase<T>: FilterQuery<T>
        where T: ProductListItemBase<T>
    {
        public override IOrderedQueryable<T> Sort(IQueryable<T> queryable)
        {
            if (string.Equals(Order, nameof(ProductListItemBase<T>.CreatedString), 
                StringComparison.InvariantCultureIgnoreCase))
            {
                return Asc ? queryable.OrderByDescending(x => x.Created) : queryable.OrderBy(x => x.Created);
            }
            
            return base.Sort(queryable);
        }
    }
}