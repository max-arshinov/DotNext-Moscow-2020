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
        public ActionResult<IEnumerable<GetBestsellersListItem>> Get(
            [FromServices] IQueryHandler<GetBestsellersQuery, IEnumerable<GetBestsellersListItem>> handler,
            [FromQuery] GetBestsellersQuery query)
        {
            return Ok(handler.Handle(query));
        }

        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(NewArrivalsListItem), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NewArrivalsListItem>> Get(
            [FromServices] IQueryable<Product> _products,
            [FromQuery] GetNewArrivals query)
        {
            var products = _products
                .ProjectToType<NewArrivalsListItem>()
                .OrderByDescending(x => x.DateCreated)
                .Take(4)
                .ToList();
            return Ok(products);
        }

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<SaleListItem>> Get(
            [FromServices] IQueryable<Product> _products,
            [FromQuery] GetSale query)
        {
            var products = _products
                .Where(x => x.DiscountPercent > 0)
                .ProjectToType<SaleListItem>()
                .ToList();

            return Ok(products);
        }
    }
}