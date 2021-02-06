using HightechAngular.Orders.Base;

namespace HightechAngular.Web.Features.Account
{
    public class CompleteOrder : ChangeOrderStateBase
    {
        public int OrderId { get; set; }
    }
}