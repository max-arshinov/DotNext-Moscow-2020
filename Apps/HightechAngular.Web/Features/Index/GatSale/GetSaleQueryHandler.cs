using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Index.GatSale
{
    public class GetSaleQueryHandler : IQueryHandler<GetSaleQuery, IEnumerable<GetSaleListItem>>
    {
        private readonly IQueryable<Product> _products;
        public GetSaleQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }
        public IEnumerable<GetSaleListItem> Handle(GetSaleQuery input) =>
            _products
                .Where(x => x.DiscountPercent > 0)
                .ProjectToType<GetSaleListItem>();
    }
}
