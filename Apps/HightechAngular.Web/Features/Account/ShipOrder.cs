using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ShipOrder: ChangeOrderStateBase
    {
        public int   OrderId { get; set; }
    }
}