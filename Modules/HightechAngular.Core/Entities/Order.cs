using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Force.Ddd;
using HightechAngular.Identity.Entities;

namespace HightechAngular.Orders.Entities
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Order : HasIdBase
    {
        public static readonly OrderSpecs Specs = new OrderSpecs();

        protected Order() { }

        public Order(Cart cart)
        {
            User = cart.User ?? throw new InvalidOperationException("User must be authenticated");

            _orderItems = cart
                .CartItems
                .Select(x => new OrderItem(this, x))
                .ToList();

            Total = _orderItems.Select(x => x.Price).Sum();
            Status = OrderStatus.New;
        }
        
        [Required]
        public virtual User User { get; set; } = default!;

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime Updated { get; set; }

        private List<OrderItem> _orderItems = new List<OrderItem>();

        public virtual IEnumerable<OrderItem> OrderItems => _orderItems;

        public double Total { get; set; }

        public Guid? TrackingCode { get; set; }

        public OrderStatus Status { get; set; }

        public string Complaint { get; set; }

        public string AdminComment { get;  set; }
    }
}