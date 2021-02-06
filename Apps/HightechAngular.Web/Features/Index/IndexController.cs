using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Features.Index;
using HightechAngular.Web.Features.Index.GatSale;
using HightechAngular.Web.Features.Index.GetArrival;
using HightechAngular.Web.Features.Index.GetBestSellers;
using Infrastructure.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Index
{
    [Route("api")]
    public class IndexController: ApiControllerBase
    {

        [HttpGet("Bestsellers")]
        public ActionResult<IEnumerable<GetBestsellersListItem>> Get(
            [FromServices] IQueryHandler<GetBestsellersQuery, IEnumerable<GetBestsellersListItem>> handler,
            [FromQuery] GetBestsellersQuery query)=>
             Ok(handler.Handle(query));


        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(GetNewArrivalsListItem), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GetNewArrivalsListItem>> Get(
            [FromServices] IQueryHandler<GetNewArrivalsQuery, IEnumerable<GetNewArrivalsListItem>> handler,
            [FromQuery] GetNewArrivalsQuery query) =>
                Ok(handler.Handle(query));

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<GetSaleListItem>> Get(
            [FromServices] IQueryHandler<GetSaleQuery, IEnumerable<GetSaleListItem>> handler,
            [FromQuery] GetSaleQuery query) =>
                Ok(handler.Handle(query));
    }
}