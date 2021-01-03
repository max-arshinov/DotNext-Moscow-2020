using JetBrains.Annotations;

namespace HightechAngular.Orders.Base
{
    public interface IHasOrderId
    {
        [UsedImplicitly]
        int OrderId { get; }
    }
}