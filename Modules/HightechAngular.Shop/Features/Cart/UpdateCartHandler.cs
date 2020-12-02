using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Cart
{
    [UsedImplicitly]
    public class UpdateCartHandler : ICommandHandler<UpdateCartContext>
    {
        private readonly ICartStorage _cartStorage;

        public UpdateCartHandler(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }

        public void Handle(UpdateCartContext input)
        {
            _cartStorage.Cart.AddProduct(input.Product);
            _cartStorage.SaveChanges();
        }
    }
}