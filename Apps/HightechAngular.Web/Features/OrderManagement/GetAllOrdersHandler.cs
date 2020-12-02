using System.Linq;
using HightechAngular.Orders.Entities;
using HightechAngular.Shop.Features.MyOrders;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Admin.Features.OrderManagement
{
    [UsedImplicitly]
    public class GetAllOrdersHandler : GetIntEnumerableQueryHandlerBase<GetAllOrders, Order, OrderListItem>
    {
        public GetAllOrdersHandler(IQueryable<Order> queryable) : base(queryable)
        {
        }

        protected override IQueryable<OrderListItem> Map(IQueryable<Order> queryable, GetAllOrders query) =>
            queryable.Select(OrderListItem.Map);
    }
}