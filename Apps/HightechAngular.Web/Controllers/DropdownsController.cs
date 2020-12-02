using System.Threading.Tasks;
using Infrastructure.AspNetCore;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using Infrastructure.SwaggerSchema.TypeProvider;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class DropdownsController : ApiControllerBase
    {
        [HttpGet("{type}")]
        public async Task<ActionResult<DropdownOptions>> Get(
            [FromServices] ITypeProvider typeProvider,
            [FromServices] IDropdownProvider dropdownProvider,
            [FromRoute] string type)
        {
            var t = typeProvider.GetType(type);
            if (t == null)
            {
                return NotFound();
            }

            var res = await dropdownProvider.GetDropdownOptionsAsync(t);
            return Ok(res);
        }
    }
}