using System.Diagnostics;
using System.Linq;
using System.Transactions;
using Force.Reflection;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Pages.Shared;
using Infrastructure.Ddd.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HightechAngular.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult Test([FromServices] DbContext dbc)
        {
            var o = dbc
                .Set<Order>()
                .OrderByDescending(x => x.Id)
                .First();

            var status = Type<Order>.PropertySetter<OrderStatus>("Status").Compile();
            status(o, OrderStatus.New);
            dbc.SaveChanges();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}