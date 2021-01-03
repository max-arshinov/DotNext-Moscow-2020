using System;
using System.Collections.Generic;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Services
{
    public class CartDto
    {
        public Guid Id { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}