using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Force.Ccc;
using HightechAngular.Orders.Services;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class CreateOrderValidator: IValidator<CreateOrder>
    {
        private readonly ICartStorage _cartStorage;
        private static readonly ValidationResult CartIsEmpty = new ValidationResult("Cart is empty");

        public CreateOrderValidator(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }

        public IEnumerable<ValidationResult> Validate(CreateOrder obj)
        {
            yield return _cartStorage.Cart.CartItems.Any()
                ? ValidationResult.Success
                : CartIsEmpty;
        }
    }
}