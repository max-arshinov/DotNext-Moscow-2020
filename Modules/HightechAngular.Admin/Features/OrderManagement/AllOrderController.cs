using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using HightechAngular.Shop.Features.MyOrders;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class OrderController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<OrderListItem>> GetAll([FromQuery] GetAllOrders query)
        {
            return this.Process(query);
        }

        [HttpPut("PayOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PayOrder(
            [FromBody] PayOrder command,
            [FromServices] Func<PayOrder, ChangeOrderStateConext<PayOrder, Order.New>> factory)
        {
            return await this.ProcessAsync(factory(command));
        }

        [HttpGet("GetOrders")]
        [ProducesResponseType(typeof(AllOrdersItem), StatusCodes.Status200OK)]
        public IActionResult GetOrders([FromQuery] GetMyOrders query)
        {
            return this.Process(query);
        }

        [HttpPut("Shipped")]
        public async Task<IActionResult> Shipped([FromBody] ShipOrder command)
        {
            return await this.ProcessAsync(command);
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteOrderAdmin command)
        {
            return await this.ProcessAsync(command);
        }
    }
}