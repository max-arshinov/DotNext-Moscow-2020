using Force.Cqrs;
using HightechAngular.Orders.Base;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class GetMyOrders : HasCreatedFilterQuery<MyOrdersListItem> { }
}