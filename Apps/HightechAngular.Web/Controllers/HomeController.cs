using System.Diagnostics;
using System.Linq;
using System.Transactions;
using HightechAngular.Orders.Entities;
using Infrastructure.Ddd.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HightechAngular.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public IActionResult Test([FromServices] DbContext dbContext)
        {
            using (var tr = new TransactionScope())
            {
                var c = new Category("Name");
                dbContext.Add(c);
                dbContext.SaveChanges();


                return Ok(dbContext.Set<Category>().ToList());
            }
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}