using System.Linq;
using HightechAngular.Identity.Entities;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Services
{
    public static class CartExtensions
    {
        internal static CartDto ToDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                CartItems = cart.CartItems.ToList()
            };
        }

        internal static Cart FromDto(this CartDto dto, User? user)
        {
            return new Cart(dto.Id, dto.CartItems, user);
        }
    }
}