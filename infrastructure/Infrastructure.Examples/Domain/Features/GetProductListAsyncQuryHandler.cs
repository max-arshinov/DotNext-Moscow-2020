using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Examples.Domain.Features
{
    public class GetProductListAsyncQuryHandler: IQueryHandler<GetProductListAsync, Task<IEnumerable<ProductListItem>>>
    {
        private readonly IQueryable<Product> _products;

        public GetProductListAsyncQuryHandler(IQueryable<Product> products)
        {
            _products = products;
        }

        public async Task<IEnumerable<ProductListItem>> Handle(GetProductListAsync input) =>
            await _products
                .Select(ProductListItem.Map)
                .ToListAsync();
    }
}