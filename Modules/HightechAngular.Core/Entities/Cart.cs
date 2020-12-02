using System;
using System.Collections.Generic;
using System.Linq;
using HightechAngular.Identity.Entities;
using Infrastructure.Ddd;

namespace HightechAngular.Orders.Entities
{
    public class Cart: EntityBase<Guid>
    {
        public Cart(User user)
        {
            User = user;
            Id = Guid.NewGuid();
            _cartItems = new List<CartItem>();
        }

        public Cart(Guid id, IEnumerable<CartItem> cartItems, User user)
        {
            User = user;
            Id = id;
            _cartItems = new List<CartItem>(cartItems);
        }

        public User User { get; }

        private readonly List<CartItem> _cartItems;

        public IEnumerable<CartItem> CartItems => _cartItems;


        public bool TryRemoveProduct(int productId)
        {
            var ci = _cartItems
                .FirstOrDefault(x => x.ProductId == productId);
            if (ci == null)
            {
                return false;
            }
            
            if (ci.Count > 1)
            {
                ci.Count--;
            }
            else
            {
                _cartItems.Remove(ci);
            }

            return true;
        }
        
        public void AddProduct(Product product)
        {
            var ci = _cartItems
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (ci == null)
            {
                ci = new CartItem
                {
                    ProductId = product.Id,
                    Price = product.GetDiscountedPrice(),
                    ProductName = product.Name,
                    CategoryName = product.Category.Name,
                    Count = 1
                };
                
                _cartItems.Add(ci);
            }
            else
            {
                ci.Count++;
            }
        }

        public bool IsEmpty() => !_cartItems.Any();
    }
}