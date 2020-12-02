using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Orders.Handlers;
using Infrastructure.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace HightechAngular.Orders
{
    public static class OrderRegistration
    {
        public static void RegisterOrder(this IServiceCollection services)
        {
            services.AddScoped<IHandler<Order.New, Task<Order.Paid>>, PayOrderHandler>();
            services.AddScoped<IHandler<Order.Paid, Task<Order.Shipped>>, ShipOrderHandler>();
            services.AddScoped<IHandler<Order.Shipped, Task<Order.Dispute>>, DisputeOrderHandler>();
            services.AddScoped<IHandler<Order.Shipped, Task<Order.Complete>>, CompleteFromShippedOrderHandler>();
            services.AddScoped<IHandler<Order.Dispute, Task<Order.Complete>>, CompleteFromDisputeOrderHandler>();
            
            services.AddScoped<IHandler<IEnumerable<ProductPurchased>>, OrderDomainEventHandler>();
        }
    }
}