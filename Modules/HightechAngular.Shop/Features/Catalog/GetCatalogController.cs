using System.Collections.Generic;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Catalog
{
    public class CatalogController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductListItem>> Get([FromQuery] GetProducts query)
        {
            return this.Process(query);
        }

        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<CategoryListItem>> GetCategories()
        {
            return this.Process(new GetCategories());
        }
    }
}