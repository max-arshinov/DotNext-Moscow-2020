using HightechAngular.Orders.Base;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class CompleteOrderAdmin: ChangeOrderStateBase
    {
        public int   OrderId { get; set; }
    }
}