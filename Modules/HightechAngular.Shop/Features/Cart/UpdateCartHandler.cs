using Force.Ccc;
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
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartHandler(ICartStorage cartStorage, IUnitOfWork unitOfWork)
        {
            _cartStorage = cartStorage;
            _unitOfWork = unitOfWork;
        }

        public void Handle(UpdateCart input)
        {
            _cartStorage.Cart.AddProduct(_unitOfWork.Find<Product>(input.ProductId));
            _cartStorage.SaveChanges();
        }
    }
}