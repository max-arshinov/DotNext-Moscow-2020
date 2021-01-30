using System.Collections.Generic;
using System.Linq;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Dto;
using Infrastructure.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Features.Index
{
    [Route("api")]
    public class IndexController: ApiControllerBase
    {
        private readonly IQueryable<Product> _products;

        public IndexController(IQueryable<Product> products)
        {
            _products = products;
        }

        [HttpGet("Bestsellers")]
        public ActionResult<IEnumerable<GetBestsellersListItem>> Get([FromQuery] GetBestsellers query)
        {
            var products = _products
                .Where(Product.Specs.IsBestseller)
                .ProjectToType<GetBestsellersListItem>();
            return Ok(products);
        }

        [HttpGet("NewArrivals")]
        [ProducesResponseType(typeof(NewArrivalsListItem), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<NewArrivalsListItem>> Get([FromQuery] GetNewArrivals query)
        {
            var products = _products
                .ProjectToType<NewArrivalsListItem>()
                .OrderByDescending(x => x.DateCreated)
                .Take(4);
            return Ok(products);
        }

        [HttpGet("Sale")]
        public ActionResult<IEnumerable<SaleListItem>> Get([FromQuery] GetSale query)
        {
            var products = _products
                .Where(x => x.DiscountPercent > 0)
                .ProjectToType<SaleListItem>();

            return Ok(products);
        }
    }
}