using System.Security.Principal;
using Force.Ddd;

namespace HightechAngular.Orders.Entities
{
    public class OrderSpecs
    {
        public OrderSpecs()
        {
        }
        
        public Spec<Order> ByUserName(string userName) => 
            new Spec<Order>(x => x.User.UserName == userName);
    }
}