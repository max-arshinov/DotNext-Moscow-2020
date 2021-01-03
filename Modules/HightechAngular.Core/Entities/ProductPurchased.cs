using System;
using Force.Ddd.DomainEvents;

namespace HightechAngular.Orders.Entities
{
    public class ProductPurchased : IDomainEvent
    {
        public ProductPurchased(int productId, int count)
        {
            ProductId = productId;
            Happened = DateTime.UtcNow;
            Count = count;
        }

        public int ProductId { get; }

        public int Count { get; }

        public DateTime Happened { get; }
    }
}