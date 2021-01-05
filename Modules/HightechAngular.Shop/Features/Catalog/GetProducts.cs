using System;
using System.Linq;
using Force.Cqrs;

namespace HightechAngular.Shop.Features.Catalog
{
    public class GetProducts : FilterQuery<ProductListItem>
    {
        public GetProducts()
        {
            Order = "Id";
            CategoryId = 1;
        }

        // ReSharper disable UnusedMember.Global
        public string[] Name { get; set; }
        
        public double[] Price { get; set; }
        // ReSharper restore UnusedMember.Global

        public int? CategoryId { get; set; }

        public override IQueryable<ProductListItem> Filter(IQueryable<ProductListItem> queryable)
        {
            return base.Filter(CategoryId.HasValue 
                ? queryable.Where(x => x.CategoryId == CategoryId)
                : queryable);
        }

        public override IOrderedQueryable<ProductListItem> Sort(IQueryable<ProductListItem> queryable)
        {
            if (string.Equals(Order, nameof(ProductListItem.CreatedString),
                StringComparison.InvariantCultureIgnoreCase))
            {
                return Asc
                    ? queryable.OrderByDescending(x => x.Created)
                    : queryable.OrderBy(x => x.Created);
            }

            return base.Sort(queryable);
        }
    }
}