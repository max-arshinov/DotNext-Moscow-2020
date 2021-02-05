using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Force.Cqrs;
using Force.Extensions;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Web.Features.Cart
{
    public class CartController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<List<CartItem>> Get([FromServices] ICartStorage storage) =>
            storage.Cart.CartItems.PipeTo(Ok);

        [HttpPut("Add")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Add(
            [FromServices] ICommandHandler<AddProductInCartCommand> commandHandler,
            [FromBody] int productId)
        {
            commandHandler.Handle(new AddProductInCartCommand(productId));
            return Ok(productId);
        }

        [HttpPut("Remove")]
        public ActionResult<bool> Remove(
            [FromServices] ICommandHandler<RemoveProductInCartCommand, bool> commandHandler,
            [FromBody] int productId)
        {
            return commandHandler.Handle(new RemoveProductInCartCommand(productId));
        }
    }
}