using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HightechAngular.Orders.Entities;
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
            [FromServices] Func<PayOrder, PayOrderContext> factory) =>
            await this.ProcessAsync(factory(command));


        [HttpPut("Ship")]
        public async Task<IActionResult> Ship(
            [FromBody] ShipOrder command,
            [FromServices] Func<ShipOrder, ShipOrderContext> factory) =>
            await this.ProcessAsync(factory(command));
    }
}