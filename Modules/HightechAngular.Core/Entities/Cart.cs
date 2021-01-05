using System;
using System.Collections.Generic;
using System.Linq;
using HightechAngular.Identity.Entities;
using Infrastructure.Ddd;

namespace HightechAngular.Orders.Entities
{
    public sealed class Cart : EntityBase<Guid>
    {
        private readonly List<CartItem> _cartItems;

        internal Cart(User? user)
        {
            Id = Guid.NewGuid();
            User = user;
            _cartItems = new List<CartItem>();
        }

        internal Cart(Guid id, IEnumerable<CartItem> cartItems, User? user)
        {
            User = user;
            Id = id;
            _cartItems = new List<CartItem>(cartItems);
        }

        public User? User { get; }

        public IEnumerable<CartItem> CartItems => _cartItems;


        public bool TryRemoveProduct(int productId)
        {
            var ci = _cartItems
                .FirstOrDefault(x => x.ProductId == productId);
            if (ci == null)
            {
                return false;
            }

            
            if (!ci.TryDecreaseCount(out var remaining))
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
                ci = new CartItem(product);
                _cartItems.Add(ci);
            }
            else
            {
                ci.IncreaseCount();
            }
        }

        public bool IsEmpty()
        {
            return !_cartItems.Any();
        }
    }
}