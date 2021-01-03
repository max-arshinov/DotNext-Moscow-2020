using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Admin.Features.OrderManagement
{
    [UsedImplicitly]
    public class GetAllOrdersHandler : GetIntEnumerableQueryHandlerBase<GetAllOrders, Order, AllOrdersListItem>
    {
        public GetAllOrdersHandler(IQueryable<Order> queryable) : base(queryable) { }
    }
}