using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Cart
{
    [UsedImplicitly]
    public class RemoveCartItemHandler : ICommandHandler<RemoveCartItem, bool>
    {
        private readonly ICartStorage _cartStorage;
        private readonly IQueryable<Product> _products;

        public RemoveCartItemHandler(ICartStorage cartStorage, 
            IQueryable<Product> products)
        {
            _cartStorage = cartStorage;
            _products = products;
        }

        public bool Handle(RemoveCartItem input)
        {
            var product = _products.First(x => x.Id == input.ProductId);
            var res = _cartStorage.Cart.TryRemoveProduct(product.Id);
            _cartStorage.SaveChanges();
            return res;
        }
    }
}