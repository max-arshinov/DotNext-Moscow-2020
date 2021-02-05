using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Features.Index;
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
            [FromQuery] GetBestsellersQuery query)
        {
            IHandler<string, int> h;
            //ICommandHandler<string, int> ch;
            //IQueryHandler<string, int> qh;
          
            return Ok(handler.Handle(query));
        }

        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(NewArrivalsListItem), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NewArrivalsListItem>> Get(
            [FromServices] IQueryable<Product> products,
            [FromQuery] GetNewArrivals query)
        {
            var res = products
                .ProjectToType<NewArrivalsListItem>()
                .OrderByDescending(x => x.DateCreated)
                .Take(4)
                .ToList();
            return Ok(res);
        }

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<SaleListItem>> Get(
            [FromServices] IQueryable<Product> products,
            [FromQuery] GetSale query)
        {
            var res = products
                .Where(x => x.DiscountPercent > 0)
                .ProjectToType<SaleListItem>()
                .ToList();

            return Ok(res);
        }
    }
}