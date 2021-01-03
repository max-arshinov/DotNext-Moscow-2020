using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class MyOrdersController : ApiControllerBase
    {
        [HttpPost("CreateNew")]
        [Authorize]
        public ActionResult<int> CreateNew([FromBody] CreateOrder query)
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

        [HttpGet]
        public ActionResult<IEnumerable<MyOrdersListItem>> Get([FromQuery] GetMyOrders query)
        {
            return this.Process(query);
        }

        [HttpPut("Dispute")]
        public async Task<IActionResult> Dispute(
            [FromBody] DisputeOrder command,
            [FromServices] Func<DisputeOrder, ChangeOrderStateConext<DisputeOrder, Order.Shipped>> factory)
        {
            return await this.ProcessAsync(factory(command));
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> Complete(
            [FromBody] CompleteOrder command,
            [FromServices] Func<CompleteOrder, ChangeOrderStateConext<CompleteOrder, Order.Shipped>> factory)
        {
            return await this.ProcessAsync(factory(command));
        }
    }
}