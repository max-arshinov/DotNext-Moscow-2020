using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Dto;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Index.Sale
{
    public class GetSaleQueryHandler :
        IQueryHandler<GetSale, IEnumerable<SaleListItem>>
    {
        private readonly IQueryable<Product> _products;
        public GetSaleQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }
        public IEnumerable<SaleListItem> Handle(GetSale input) =>
            _products
                .Where(x => x.DiscountPercent > 0)
                .ProjectToType<SaleListItem>()
                .ToList();
    }
}
