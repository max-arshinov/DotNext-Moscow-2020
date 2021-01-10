using Force.Cqrs;

namespace HightechAngular.Shop.Features.MyOrders
{
    public record CreateOrder : ICommand<int> { }
}