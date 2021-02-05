using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Dto;
using HightechAngular.Web.Features.Index.GetBestSellers;
using Infrastructure.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Features.Index
{
    [Route("api")]
    public class IndexController: ApiControllerBase
    {
        [HttpGet("Bestsellers")]
        public ActionResult<IEnumerable<BestsellersListItem>> Get(
            [FromServices] IQueryHandler<GetBestsellersQuery, IEnumerable<BestsellersListItem>> handler,
            [FromQuery] GetBestsellersQuery query)
        {
            return Ok(handler.Handle(query));
        }

        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(IEnumerable<NewArrivalsListItem>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NewArrivalsListItem>> Get(
            [FromServices] IQueryHandler<GetNewArrivals, IEnumerable<NewArrivalsListItem>> handler,
            [FromQuery] GetNewArrivals query)
        {
            return Ok(handler.Handle(query));
        }

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<SaleListItem>> Get(
            [FromServices] IQueryHandler<GetSale, IEnumerable<SaleListItem>> handler,
            [FromQuery] GetSale query)
        {
            return Ok(handler.Handle(query));
        }
    }
}