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

        public OrderStatus BecomePaid()
        {
            Status = OrderStatus.Paid;
            return Status;
        }

        public OrderStatus BecomeShipped(Guid trackingCode)
        {
            TrackingCode = trackingCode;
            Status = OrderStatus.Shipped;
            return Status;
        }

        public OrderStatus BecomeDisputed(string complaint)
        {
            Complaint = complaint;
            Status = OrderStatus.Dispute;
            return Status;
        }

        public OrderStatus BecomeComplete()
        {
            Status = OrderStatus.Complete;
            return Status;
        }

        [Required]
        public virtual User User { get; protected set; } = default!;

        public DateTime Created { get; protected set; } = DateTime.UtcNow;

        public DateTime Updated { get; protected set; }

        private List<OrderItem> _orderItems = new List<OrderItem>();

        public virtual IEnumerable<OrderItem> OrderItems => _orderItems;

        public double Total { get; protected set; }

        public Guid? TrackingCode { get; protected set; }

        public OrderStatus Status { get; protected set; }

        public string? Complaint { get; protected set; }

        public string? AdminComment { get; protected set; }
    }
}