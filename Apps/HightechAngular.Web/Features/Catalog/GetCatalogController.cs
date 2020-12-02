using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Catalog
{
    public class CatalogController: ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ProductListItem), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] GetProducts query) => 
            this.Process(query);

        [HttpGet("GetCategories")]
        public IActionResult GetCategories() => this.Process(new GetCategories());
    }
}