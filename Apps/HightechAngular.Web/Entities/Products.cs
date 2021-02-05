using System.Linq;
using Force.Cqrs;
using HightechAngular.Web.Features.Shared;

namespace HightechAngular.Shop.Features.Catalog
{
    public class GetProducts: FilterQuery<ProductListItem>
    {
        public string[] Name { get; set; }
        public double[] Price { get; set; }
        public int CategoryId { get; set; }
        public GetProducts()
        {
            Order = "Id";
            CategoryId = 1;
        }

        public override IQueryable<ProductListItem> Filter(IQueryable<ProductListItem> queryable)
        {
            return base.Filter(queryable.Where(x => x.CategoryId == CategoryId));
        }

        public override IOrderedQueryable<ProductListItem> Sort(IQueryable<ProductListItem> queryable)
        {
            if (Order == "dateCreated")
            {
                return Asc ? queryable.OrderByDescending(x => x.DateCreated) : queryable.OrderBy(x => x.DateCreated);
            }
            return base.Sort(queryable);
        }
    }
}