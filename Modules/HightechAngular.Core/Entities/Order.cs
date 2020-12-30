using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Force.Ddd.DomainEvents;
using HightechAngular.Identity.Entities;
using Infrastructure.Ddd.Domain.State;

namespace HightechAngular.Orders.Entities
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
    public partial class Order :
        HasStateBase<OrderStatus, Order.OrderStateBase>,
        IHasDomainEvents
    {
        public static readonly OrderSpecs Specs = new OrderSpecs();
        private static DomainEventStore _domainEvents = new DomainEventStore();

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        protected Order() { }

        public Order(Cart cart)
        {
            User = cart.User ?? throw new InvalidOperationException("User must be authenticated");

            _orderItems = cart
                .CartItems
                .Select(x => new OrderItem(this, x)
                {
                    Order = this,
                    Count = x.Count,
                    Name = x.ProductName,
                    Price = x.Price
                })
                .ToList();

            Total = _orderItems.Select(x => x.Price).Sum();
            Status = OrderStatus.New;
        }

        [Required]
        public virtual User User { get; protected set; }

        public DateTime Created { get; protected set; } = DateTime.UtcNow;


        public DateTime Updated { get; protected set; }

        public virtual IEnumerable<OrderItem> OrderItems => _orderItems;

        public double Total { get; protected set; }

        public Guid? TrackingCode { get; protected set; }

        public string Complaint { get; protected set; }

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            var domainEvents = _domainEvents;
            _domainEvents = new DomainEventStore();
            return domainEvents;
        }

        public override OrderStateBase GetState(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.New => new New(this),
                OrderStatus.Paid => new Paid(this),
                OrderStatus.Shipped => new Shipped(this),
                OrderStatus.Dispute => new Disputed(this),
                OrderStatus.Complete => new Complete(this),

                //https://github.com/dotnet/csharplang/issues/2266
                //see also https://github.com/ardalis/SmartEnum
                _ => throw new NotSupportedException($"Status \"{status}\" is not supported")
            };
        }
    }
}