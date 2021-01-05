using Force.Ddd;

namespace HightechAngular.Orders.Entities
{
    public class OrderSpecs
    {
        internal OrderSpecs() { }

        public Spec<Order> ByUserName(string userName)
        {
            return new Spec<Order>(x => x.User.UserName == userName);
        }
    }
}