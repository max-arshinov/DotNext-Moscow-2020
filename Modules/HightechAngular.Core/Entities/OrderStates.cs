using System;
using Infrastructure.Ddd.Domain.State;

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
                return Entity.To<Paid>(OrderStatus.Paid);
            }
        }

        public class Paid : OrderStateBase
        {
            public Paid(Order entity) : base(entity)
            {
            }

            internal Shipped BecomeShipped()
            {
                return Entity.To<Shipped>(OrderStatus.Shipped);
            }

            public override OrderStatus EligibleStatus => OrderStatus.Paid;
        }

        public class Shipped : OrderStateBase
        {
            public Shipped(Order entity) : base(entity)
            {
            }

            internal Dispute BecomeDispute()
            {
                return Entity.To<Dispute>(OrderStatus.Dispute);
            }

            internal Complete BecomeComplete()
            {
                return Entity.To<Complete>(OrderStatus.Complete);
            }

            public override OrderStatus EligibleStatus => OrderStatus.Shipped;
        }

        public class Complete : OrderStateBase
        {
            public Complete(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Complete;
        }

        public class Dispute : OrderStateBase
        {
            public Dispute(Order entity) : base(entity)
            {
            }

            internal Complete BecomeComplete()
            {
                return Entity.To<Complete>(OrderStatus.Complete);
            }

            public override OrderStatus EligibleStatus => OrderStatus.Dispute;
        }
    }
}