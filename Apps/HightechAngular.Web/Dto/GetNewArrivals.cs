using Force.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Dto
{
    public class GetNewArrivals : FilterQuery<NewArrivalsListItem>
    {
        public override IOrderedQueryable<NewArrivalsListItem> Sort(IQueryable<NewArrivalsListItem> queryable)
        {
            if (Order == "dateCreated")
            {
                return Asc ? queryable.OrderByDescending(x => x.DateCreated) : queryable.OrderBy(x => x.DateCreated);
            }
            return base.Sort(queryable);
        }
    }
}
