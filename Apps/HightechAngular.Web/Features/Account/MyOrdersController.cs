using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Identity.Services;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using HightechAngular.Web.Features.Account;
using Infrastructure.AspNetCore;
using Infrastructure.Cqrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Features.MyOrders
{
    public class MyOrdersController : ApiControllerBase
    {
        private readonly IQueryable<Order> _orders;
        private readonly IUnitOfWork _unitOfWork;

        public MyOrdersController(IQueryable<Order> orders,
            IUnitOfWork unitOfWork)
        {
            _orders = orders;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateNew")]
        [Authorize]
        public ActionResult<int> CreateNew(
            [FromServices] ICommandHandler<CreateOrder, int> handler,
            [FromBody] CreateOrder query)
        {
            return Ok(handler.Handle(query));
        }

        [HttpGet("GetMyOrders")]
        public ActionResult<IEnumerable<OrderListItem>> GetMyOrders(
            [FromServices] IQueryHandler<GetMyOrders, IEnumerable<OrderListItem>> handler,
            [FromQuery] GetMyOrders query)
        {
            return Ok(handler.Handle(query));
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