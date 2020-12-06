using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Base
{
    public interface IHasOrderState<T>
        where T: Order.OrderStateBase
    {
        T State { get; }
    }
}