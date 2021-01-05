using HightechAngular.Identity.Services;
using HightechAngular.Orders.Entities;
using Microsoft.AspNetCore.Http;

namespace HightechAngular.Orders.Services
{
    public class CartStorage : ICartStorage
    {
        private static readonly string _cartKey = "Cart";
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserContext _userContext;

        private Cart _cart;

        public CartStorage(IHttpContextAccessor accessor, IUserContext userContext)
        {
            _accessor = accessor;
            _userContext = userContext;
        }

        public Cart Cart
        {
            get
            {
                if (_cart != null)
                {
                    return _cart;
                }
                   
                var cartDto = _accessor
                            .HttpContext
                            .Session
                            .Get<CartDto>(_cartKey);

                _cart = cartDto?.FromDto(_userContext.User) ?? new Cart(_userContext.User);

                return _cart;
            }
        }


        public void SaveChanges()
        {
            if (_cart != null)
            {
                _accessor
                    .HttpContext
                    .Session
                    .Set(_cartKey, _cart.ToDto());
            }
        }

        public void EmptyCart()
        {
            _accessor
                .HttpContext
                .Session
                .Remove(_cartKey);
        }
    }
}