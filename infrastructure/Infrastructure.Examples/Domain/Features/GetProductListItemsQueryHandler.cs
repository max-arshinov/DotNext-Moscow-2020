using System.Linq;
using Infrastructure.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class GetProductListItemsQueryHandler :
        GetIntEnumerableQueryHandlerBase<GetProductList, Product, ProductListItem>
    {
        public GetProductListItemsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }

        protected override IQueryable<ProductListItem> Map(IQueryable<Product> queryable, GetProductList query) =>
            queryable.Select(ProductListItem.Map);
    }
}