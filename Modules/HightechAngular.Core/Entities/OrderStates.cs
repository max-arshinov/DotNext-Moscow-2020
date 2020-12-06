using System;
using Infrastructure.Ddd.Domain.State;
using JetBrains.Annotations;

namespace HightechAngular.Orders.Entities
{
    public partial class Order
    {
        public abstract class OrderStateBase : SingleStateBase<Order, OrderStatus>
        {
            protected OrderStateBase(Order entity) : base(entity)
            {
            }
        }


        public class New : OrderStateBase
        {
            public New(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.New;

            internal Paid BecomePaid()
            {
                foreach (var orderItem in Entity.OrderItems)
                    _domainEvents.Raise(new ProductPurchased(orderItem.ProductId, orderItem.Count));

                return Entity.To<Paid>(OrderStatus.Paid);
            }
        }

        public class Paid : OrderStateBase
        {
            public Paid(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Paid;

            internal Shipped BecomeShipped()
            {
                return Entity.To<Shipped>(OrderStatus.Shipped);
            }
        }

        public class Shipped : OrderStateBase
        {
            public Shipped(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Shipped;

            internal Disputed BecomeDisputed([NotNull] string complain)
            {
                if (string.IsNullOrEmpty(complain)) throw new ArgumentNullException(nameof(complain));
                Entity.Complaint = complain;
                return Entity.To<Disputed>(OrderStatus.Dispute);
            }

            internal Complete BecomeComplete()
            {
                return Entity.To<Complete>(OrderStatus.Complete);
            }
        }

        public class Complete : OrderStateBase
        {
            public Complete(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Complete;
        }

        public class Disputed : OrderStateBase
        {
            public Disputed(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Dispute;

            internal Complete Resolve(string resolutionComment)
            {
                if (string.IsNullOrEmpty(resolutionComment)) throw new ArgumentNullException(nameof(resolutionComment));
                return Entity.To<Complete>(OrderStatus.Complete);
            }
        }
    }
}