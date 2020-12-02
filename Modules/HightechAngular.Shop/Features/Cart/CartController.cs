using System;
using System.Collections.Generic;
using System.Net;
using Force.Extensions;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Services;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Cart
{
    public class CartController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<List<CartItem>> Get([FromServices] ICartStorage storage) =>
            storage.Cart.CartItems.PipeTo(Ok);

        [HttpPut("Add")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Add([FromBody] int productId,
            [FromServices] Func<UpdateCart, UpdateCartContext> factory) =>
            this.Process(factory(new UpdateCart(productId)));
        
        [HttpPut("Remove")]
        public ActionResult<bool> Remove([FromBody] int productId,
            [FromServices] Func<RemoveCartItem, RemoveCartItemContext> factory) =>
            this.Process(factory(new RemoveCartItem(productId)));
    }
}