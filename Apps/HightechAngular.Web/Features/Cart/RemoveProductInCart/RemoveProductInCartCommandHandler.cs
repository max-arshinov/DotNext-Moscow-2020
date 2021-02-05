using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Cart.RemoveProductInCart
{
    public class RemoveProductInCartCommandHandler : ICommandHandler<RemoveProductInCartCommand, bool>
    {
        private readonly ICartStorage _cartStorage;

        public RemoveProductInCartCommandHandler(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }
        public bool Handle(RemoveProductInCartCommand input)
        {
            var res = _cartStorage.Cart.TryRemoveProduct(input.ProductId);
            _cartStorage.SaveChanges();
            return res;
        }
    }
}
