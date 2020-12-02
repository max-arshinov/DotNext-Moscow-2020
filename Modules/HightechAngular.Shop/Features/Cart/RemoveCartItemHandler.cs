using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Cart
{
    [UsedImplicitly]
    public class RemoveCartItemHandler : ICommandHandler<RemoveCartItemContext, bool>
    {
        private readonly ICartStorage _cartStorage;

        public RemoveCartItemHandler(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }

        public bool Handle(RemoveCartItemContext input)
        {
            var res = _cartStorage.Cart.TryRemoveProduct(input.Product.Id);
            _cartStorage.SaveChanges();
            return res;
        }
    }
}