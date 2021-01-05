using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class OrderController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AllOrdersListItem>> GetAll([FromQuery] GetAllOrders query) => 
            this.Process(query);

        [HttpPut("Pay")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PayOrder(
            [FromBody] PayOrder command,
            [FromServices] Func<PayOrder, ChangeOrderStateConext<PayOrder, Order.New>> factory) =>
            await this.ProcessAsync(factory(command));


        [HttpPut("Ship")]
        public async Task<IActionResult> Ship(
            [FromBody] ShipOrder command,
            [FromServices] Func<ShipOrder, ChangeOrderStateConext<ShipOrder, Order.Paid>> factory) =>
            await this.ProcessAsync(factory(command));

        [HttpPut("Resolve")]
        public async Task<IActionResult> Resolve(
            [FromBody] ResolveDisputedOrder command,
            [FromServices] Func<
                ResolveDisputedOrder,
                ChangeOrderStateConext<ResolveDisputedOrder, Order.Disputed>> factory) => 
            await this.ProcessAsync(factory(command));
    }
}