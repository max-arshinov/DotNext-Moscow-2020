using Infrastructure.AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.AspNetCore
{
    [ApiController]
    [ApiControllerFilter]
    [Route("/api/[controller]/[action]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseAuthController : ControllerBase
    {
    }
}