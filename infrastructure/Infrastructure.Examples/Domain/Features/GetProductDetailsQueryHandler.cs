using System.Linq;
using Infrastructure.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class GetProductDetailsQueryHandler :
        GetOneByIntIdQueryHandlerBase<GetProductDetailsById, Product, ProductDetails>
    {
        public GetProductDetailsQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }

        protected override IQueryable<ProductDetails> Map(IQueryable<Product> queryable, GetProductDetailsById query) =>
            queryable.Select(ProductDetails.Map);
    }
}