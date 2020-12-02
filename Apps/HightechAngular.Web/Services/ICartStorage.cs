using System.Collections.Generic;
using HightechAngular.Orders.Entities;

namespace HightechAngular.Orders.Services
{
    public interface ICartStorage
    {
        Cart Cart { get; }
        void SaveChanges();
        void EmptyCart();
    }
}