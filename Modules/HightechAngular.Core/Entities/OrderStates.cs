using Infrastructure.Ddd.State;

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

            public Paid BecomePaid()
            {
                return Entity.To<Paid>(OrderStatus.Paid);
            }
        }

        public class Paid : OrderStateBase
        {
            public Paid(Order entity) : base(entity)
            {
            }

            public Shipped BecomeShipped()
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

            public Disputed BecomeDispute()
            {
                return Entity.To<Disputed>(OrderStatus.Dispute);
            }

            public Complete BecomeComplete()
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

        public class Disputed : OrderStateBase
        {
            public Disputed(Order entity) : base(entity)
            {
            }

            public Complete BecomeComplete()
            {
                return Entity.To<Complete>(OrderStatus.Complete);
            }

            public override OrderStatus EligibleStatus => OrderStatus.Dispute;
        }
    }
}