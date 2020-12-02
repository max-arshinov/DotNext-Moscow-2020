using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;

namespace HightechAngular.Shop.Features.Index
{
    public class GetSaleQueryHandler: GetIntEnumerableQueryHandlerBase<GetSale, Product, SaleListItem>
    {
        public GetSaleQueryHandler(IQueryable<Product> queryable) : base(queryable)
        {
        }
    }
}