using System.Linq;
using Force.Cqrs;
using Force.Linq;
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

        public RemoveCartItemHandler(ICartStorage cartStorage, IQueryable<Product> products)
        {
            _cartStorage = cartStorage;
            _products = products;
        }

        public bool Handle(RemoveCartItem input)
        {
            var res = _cartStorage.Cart.TryRemoveProduct(input.ProductId);
            _cartStorage.SaveChanges();
            return res;
        }
    }
}