using System.Linq;
using Force.Cqrs;

namespace HightechAngular.Web.Features.Index.GatSale
{
    public class GetSaleQuery : FilterQuery<GetSaleListItem>
    {
        public override IOrderedQueryable<GetSaleListItem> Sort(IQueryable<GetSaleListItem> queryable)
        {
            if (Order == "dateCreated")
            {
                return Asc ? queryable.OrderByDescending(x => x.DateCreated) : queryable.OrderBy(x => x.DateCreated);
            }
            return base.Sort(queryable);
        }
    }
}