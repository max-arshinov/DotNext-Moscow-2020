using System.Linq;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Cart
{
    [UsedImplicitly]
    public class UpdateCartHandler : ICommandHandler<UpdateCart>
    {
        private readonly ICartStorage _cartStorage;
        private readonly IQueryable<Product> _products;

        public UpdateCartHandler(ICartStorage cartStorage, 
            IQueryable<Product> products)
        {
            _cartStorage = cartStorage;
            _products = products;
        }

        public void Handle(UpdateCart input)
        {
            var product = _products.First(x => x.Id == input.ProductId);
            _cartStorage.Cart.AddProduct(product);
            _cartStorage.SaveChanges();
        }
    }
}