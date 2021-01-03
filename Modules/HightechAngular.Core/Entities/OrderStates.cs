using System;
using Infrastructure.Ddd.State;

namespace HightechAngular.Orders.Entities
{
    public partial class Order
    {
        public abstract class OrderStateBase : SingleStateBase<Order, OrderStatus>
        {
            protected OrderStateBase(Order entity) : base(entity) { }
        }


        public class New : OrderStateBase
        {
            public New(Order entity) : base(entity) { }

            public override OrderStatus EligibleStatus => OrderStatus.New;

            internal Paid BecomePaid()
            {
                return Entity.To<Paid>(OrderStatus.Paid);
            }
        }

        public class Paid : OrderStateBase
        {
            public Paid(Order entity) : base(entity) { }

            public override OrderStatus EligibleStatus => OrderStatus.Paid;

            internal Shipped BecomeShipped(Guid trackingCode)
            {
                Entity.TrackingCode = trackingCode;
                return Entity.To<Shipped>(OrderStatus.Shipped);
            }
        }

        public class Shipped : OrderStateBase
        {
            public Shipped(Order entity) : base(entity) { }

            public override OrderStatus EligibleStatus => OrderStatus.Shipped;

            internal Disputed BecomeDisputed(string complaint)
            {
                if (string.IsNullOrEmpty(complaint))
                {
                    throw new ArgumentNullException(nameof(complaint));
                }

                Entity.Complaint = complaint;
                return Entity.To<Disputed>(OrderStatus.Dispute);
            }

            internal Complete BecomeComplete()
            {
                return Entity.To<Complete>(OrderStatus.Complete);
            }
        }

        public class Complete : OrderStateBase
        {
            public Complete(Order entity) : base(entity) { }

            public override OrderStatus EligibleStatus => OrderStatus.Complete;
        }

        public class Disputed : OrderStateBase
        {
            public Disputed(Order entity) : base(entity) { }

            public override OrderStatus EligibleStatus => OrderStatus.Dispute;

            internal Complete Resolve(string resolutionComment)
            {
                Entity.AdminComment = resolutionComment;
                return Entity.To<Complete>(OrderStatus.Complete);
            }
        }
    }
}