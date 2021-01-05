using System.Diagnostics;
using HightechAngular.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}