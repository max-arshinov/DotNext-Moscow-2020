using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrder, int>
    {
        private readonly ICartStorage _cartStorage;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(
            ICartStorage cartStorage, 
            IUnitOfWork unitOfWork)
        {
            _cartStorage = cartStorage;
            _unitOfWork = unitOfWork;
        }

        public int Handle(CreateOrder input)
        {
            var order = new Order(_cartStorage.Cart);

            _unitOfWork.Add(order);
            _cartStorage.EmptyCart();
            _unitOfWork.Commit();

            return order.Id;
        }
    }
}