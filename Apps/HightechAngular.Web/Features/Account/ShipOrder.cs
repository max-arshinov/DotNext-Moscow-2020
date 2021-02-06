using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Web.Features.Account
{
    public class ShipOrder : ChangeOrderStateBase
    {
        public int OrderId { get; set; }
    }
}