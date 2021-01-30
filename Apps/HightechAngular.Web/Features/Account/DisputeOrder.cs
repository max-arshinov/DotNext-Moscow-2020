using HightechAngular.Orders.Base;

namespace HightechAngular.Web.Features.MyOrders
{
    public class DisputeOrder : ChangeOrderStateBase
    {
        public int   OrderId { get; set; }
    }
}