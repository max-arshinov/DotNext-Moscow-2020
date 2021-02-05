using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Features.Shared;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Catalog
{
    public class GetProductsQueryHandler :
        IQueryHandler<GetProducts, IEnumerable<ProductListItem>>
    {
        private readonly IQueryable<Product> _products;
        public GetProductsQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }
        public IEnumerable<ProductListItem> Handle(GetProducts input) =>
            _products
                .Where(x => x.Category.Id == input.CategoryId)
                .ProjectToType<ProductListItem>()
                .ToList();
    }
}
