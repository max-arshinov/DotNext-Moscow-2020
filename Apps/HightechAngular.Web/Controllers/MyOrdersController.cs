using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using HightechAngular.Identity.Services;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using Infrastructure.AspNetCore;
using Infrastructure.Cqrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class MyOrdersController : ApiControllerBase
    {
        private readonly IQueryable<Order> _orders;
        private readonly IUserContext _userContext;
        private readonly ICartStorage _cartStorage;
        private readonly IUnitOfWork _unitOfWork;

        public MyOrdersController(IQueryable<Order> orders, 
            IUserContext userContext, 
            ICartStorage cartStorage, 
            IUnitOfWork unitOfWork)
        {
            _orders = orders;
            _userContext = userContext;
            _cartStorage = cartStorage;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateNew")]
        [Authorize]
        public ActionResult<int> CreateNew([FromBody] CreateOrder query)
        {
            var order = new Order(_cartStorage.Cart);

            _unitOfWork.Add(order);
            _cartStorage.EmptyCart();
            _unitOfWork.Commit();

            return Ok(order.Id);
        }

        [HttpGet("GetMyOrders")]
        public ActionResult<IEnumerable<OrderListItem>> GetMyOrders([FromQuery] GetMyOrders query)
        {
            var orders = _orders
                .Where(Order.Specs.ByUserName(_userContext.User?.UserName))
                .Select(OrderListItem.Map);
            return Ok(orders);
        }

        [HttpPut("Dispute")]
        public async Task<IActionResult> Dispute([FromBody] DisputeOrder command)
        {
            var order = _orders.First(x => x.Id == command.OrderId);
            await Task.Delay(1000);
            var result = order.BecomeDispute();
            return Ok(new HandlerResult<OrderStatus>(result));
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteOrder command)
        {
            var order = _orders.First(x => x.Id == command.OrderId);
            await Task.Delay(1000);
            var result = order.BecomeComplete();
            return Ok(new HandlerResult<OrderStatus>(result));
        }

        [HttpPut("PayOrder")]
        public async Task<IActionResult> PayOrder([FromBody] PayMyOrder command)
        {
            var order = _orders.First(x => x.Id == command.OrderId);
            await Task.Delay(1000);
            var result = order.BecomePaid();
            _unitOfWork.Commit();
            return Ok(new HandlerResult<OrderStatus>(result));
        }
    }
}