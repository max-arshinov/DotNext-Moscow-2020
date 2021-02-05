using System;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Extensions;
using HightechAngular.Admin.Features.OrderManagement;
using HightechAngular.Orders.Entities;
using HightechAngular.Shop.Features.Cart;
using HightechAngular.Shop.Features.MyOrders;
using HightechAngular.Web.Features.Account;
using Infrastructure.AspNetCore;
using Infrastructure.Cqrs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HightechAngular.Web.Features.Admin
{
    public class OrderController : ApiControllerBase
    {
        private readonly IQueryable<Order> _orders;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IQueryable<Order> orders, IUnitOfWork unitOfWork)
        {
            _orders = orders;
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(OrderListItem), StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] GetAllOrders query) =>
            _orders
                .Select(AllOrdersItem.Map)
                .PipeTo(Ok);

        [HttpPut("PayOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PayOrder([FromBody] PayOrder command)
        {
            await Task.Delay(1000);
            var order = _orders.First(x => x.Id == command.OrderId);
            var result = order.BecomePaid();
            _unitOfWork.Commit();
            return Ok(new HandlerResult<OrderStatus>(result));
        }

        [HttpGet("GetOrders")]
        [ProducesResponseType(typeof(AllOrdersItem), StatusCodes.Status200OK)]
        public IActionResult GetOrders([FromQuery] GetMyOrders query) =>
            _orders
                .Select(AllOrdersItem.Map)
                .PipeTo(Ok);

        [HttpPut("Shipped")]
        public async Task<IActionResult> Shipped([FromBody] ShipOrder command)
        {
            var order = _orders.First(x => x.Id == command.OrderId);
            await Task.Delay(1000);
            var result = order.BecomeShipped();
            return Ok(new HandlerResult<OrderStatus>(result));
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteOrderAdmin command)
        {
            var order = _orders.First(x => x.Id == command.OrderId);
            await Task.Delay(1000);
            var result = order.BecomeComplete();
            return Ok(new HandlerResult<OrderStatus>(result));
        }
    }
}