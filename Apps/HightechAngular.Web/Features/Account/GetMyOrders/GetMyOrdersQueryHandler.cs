using Force.Cqrs;
using HightechAngular.Identity.Services;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Features.MyOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Account
{
    public class GetMyOrdersQueryHandler : IQueryHandler<GetMyOrders, IEnumerable<OrderListItem>>
    {
        private readonly IQueryable<Order> _orders;
        private readonly IUserContext _userContext;

        public GetMyOrdersQueryHandler(IQueryable<Order> orders,
            IUserContext userContext)
        {
            _orders = orders;
            _userContext = userContext;
        }
        public IEnumerable<OrderListItem> Handle(GetMyOrders input)=> _orders
                .Where(Order.Specs.ByUserName(_userContext.User?.UserName))
                .Select(OrderListItem.Map);
    }
}
