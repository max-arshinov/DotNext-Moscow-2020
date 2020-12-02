using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.AspNetCore
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
    }
}