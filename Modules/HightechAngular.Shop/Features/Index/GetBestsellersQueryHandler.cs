using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Index
{
    [UsedImplicitly]
    public class GetBestsellersQueryHandler: GetIntEnumerableQueryHandlerBase<GetBestsellers, Product, BestsellersListItem>
    {
        public GetBestsellersQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }
    }
}