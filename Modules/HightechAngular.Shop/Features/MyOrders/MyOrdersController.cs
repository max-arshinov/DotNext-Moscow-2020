using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HightechAngular.Orders.Entities;
using Infrastructure.AspNetCore;
using Infrastructure.OperationContext;
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
        public async Task<IActionResult> PayOrder([FromBody] PayMyOrder command)
        {
            return await this.ProcessAsync(command);
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyOrdersListItem>> Get([FromQuery] GetMyOrders query)
        {
            return this.Process(query);
        }

        [HttpPut("Dispute")]
        public async Task<IActionResult> Dispute(
            [FromBody] DisputeOrder command,
            [FromServices] Func<DisputeOrder, DisputeOrder> factory)
        {
            return await this.ProcessAsync(command);
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteOrder command)
        {
            return await this.ProcessAsync(command);
        }
    }
}