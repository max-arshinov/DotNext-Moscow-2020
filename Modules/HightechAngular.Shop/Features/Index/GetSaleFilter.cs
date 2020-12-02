using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Ddd;

namespace HightechAngular.Shop.Features.Index
{
    public class GetSaleFilter: IFilter<Product, GetSale>
    {
        public IQueryable<Product> Filter(IQueryable<Product> queryable, GetSale predicate)
        {
            return queryable.Where(x => x.DiscountPercent > 0);
        }
    }
}