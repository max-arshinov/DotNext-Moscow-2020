using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.MyOrders
{
    [UsedImplicitly]
    public class GetMyOrdersQueryHandler : GetIntEnumerableQueryHandlerBase<GetMyOrders, Order, MyOrdersListItem>
    {
        public GetMyOrdersQueryHandler(IQueryable<Order> queryable) : base(queryable) { }
    }
}