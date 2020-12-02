using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Catalog
{
    [UsedImplicitly]
    public class GetProductsQueryHandler: GetIntEnumerableQueryHandlerBase<GetProducts, Product, ProductListItem>
    {
        public GetProductsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }
    }
}