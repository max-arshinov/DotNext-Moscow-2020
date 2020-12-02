using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICartStorage _cartStorage;
        private readonly IQueryable<Product> _products;

        public CartController(ICartStorage cartStorage, 
            IQueryable<Product> products)
        {
            _cartStorage = cartStorage;
            _products = products;
        }

        [HttpGet]
        public ActionResult<List<CartItem>> Get([FromServices] ICartStorage storage) =>
            storage.Cart.CartItems.PipeTo(Ok);

        [HttpPut("Add")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Add([FromBody] int productId)
        {
            var product = _products.First(x => x.Id == productId);
            _cartStorage.Cart.AddProduct(product);
            _cartStorage.SaveChanges();
            return Ok(productId);
        }

        [HttpPut("Remove")]
        public ActionResult<bool> Remove([FromBody] int productId)
        {
            var res = _cartStorage.Cart.TryRemoveProduct(productId);
            _cartStorage.SaveChanges();
            return res;
        }
    }
}