using HightechAngular.Orders.Base;

namespace HightechAngular.Web.Features.MyOrders
{
    public class CompleteOrder : ChangeOrderStateBase
    {
        public int   OrderId { get; set; }
    }
}