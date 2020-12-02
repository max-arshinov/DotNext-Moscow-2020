using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;

namespace HightechAngular.Orders.Handlers
{
    public class OrderDomainEventHandler : IGroupDomainEventHandler<IEnumerable<ProductPurchased>>
    {
        private readonly IQueryable<Product> _products;
        public OrderDomainEventHandler(IQueryable<Product> products)
        {
            _products = products;
        }

        public void Handle(IEnumerable<ProductPurchased> input) 
        {
            var dict = input.ToDictionary(x => x.ProductId, x => x.Count);

            dict
                .Select(x => _products
                    .Where(y => y.Id == x.Key)
                    .BatchUpdate(Product.UpdatePurchaseCountExpression(dict[x.Key])))
                .ToList();
        }
    }
}