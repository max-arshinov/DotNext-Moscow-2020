using System.Collections.Generic;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Index
{
    [Route("api")]
    public class IndexController : ApiControllerBase
    {
        [HttpGet("Bestsellers")]
        public ActionResult<IEnumerable<BestsellersListItem>> Get([FromQuery] GetBestsellers query)
        {
            return this.Process(query);
        }

        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(NewArrivalsListItem), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NewArrivalsListItem>> Get([FromQuery] GetNewArrivals query)
        {
            return this.Process(query);
        }

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<SaleListItem>> Get([FromQuery] GetSale query)
        {
            return this.Process(query);
        }
    }
}