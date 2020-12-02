using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;
using Mapster;

namespace HightechAngular.Shop.Features.Index
{
    [UsedImplicitly]
    public class GetNewArrivalsQueryHandler: GetIntEnumerableQueryHandlerBase<GetNewArrivals, Product, NewArrivalsListItem>
    {
        public GetNewArrivalsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }

        protected override IQueryable<NewArrivalsListItem> Map(IQueryable<Product> queryable, GetNewArrivals query)
        {
            return queryable
                .ProjectToType<NewArrivalsListItem>()
                .OrderByDescending(x => x.DateCreated)
                .Take(4);
        }

    }
}