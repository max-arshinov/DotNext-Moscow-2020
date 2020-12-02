using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using IntEntityBase = Infrastructure.Ddd.Domain.IntEntityBase;

namespace HightechAngular.Orders.Entities
{
    public class OrderItem: IntEntityBase
    {
        [UsedImplicitly]
        public OrderItem()
        {
        }

        public OrderItem(Order order, CartItem cartItem)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Order = order;
            Count = cartItem.Count;
            Name = cartItem.ProductName;
            Price = cartItem.Price;
            ProductId = cartItem.ProductId;
        }
        
        [Required]
        public string Name { get; set; }
        
        public virtual Order Order { get; set; }
        
        public double Price { get; set; }
        
        [Obsolete]
        public int DiscountPercent { get; set; }
        
        public int Count { get; set; }
        
        public int ProductId { get; set; }
    }
}