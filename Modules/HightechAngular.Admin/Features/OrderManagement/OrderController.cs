using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> PayOrder([FromBody] PayOrder command) =>
            await this.ProcessAsync(command);


        [HttpPut("Ship")]
        public async Task<IActionResult> Ship([FromBody] ShipOrder command) =>
            await this.ProcessAsync(command);
        
        [HttpPut("Resolve")]
        public async Task<IActionResult> Resolve([FromBody] ResolveDisputedOrder command)
        {
            return await this.ProcessAsync(command);
        }
    }
}