using System;
using System.Collections.Generic;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Services
{
    internal class CartDto
    {
        public Guid Id { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}