using System.Collections.Generic;
using System.Linq;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Features.Shared;
using Infrastructure.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Features.Catalog
{
    public class CatalogController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductListItem>), StatusCodes.Status200OK)]
        public IActionResult Get(
            [FromServices] IQueryHandler<GetProducts, IEnumerable<ProductListItem>> handler,
            [FromQuery] GetProducts query)
        {
            return Ok(handler.Handle(query));
        }


        [HttpGet("GetCategories")]
        public IActionResult GetCategories([FromServices] IQueryable<Category> categories)
        {
            return Ok(categories);
        }
    }
}