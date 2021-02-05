using HightechAngular.Orders.Base;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class CompleteOrder : ChangeOrderStateBase
    {
        public int   OrderId { get; set; }
    }
}