using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Force.Expressions;
using Force.Extensions;
using Infrastructure.Ddd;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Orders.Entities
{
    public class Product : IntEntityBase
    {
        public static readonly Expression<Func<Product, double>> DiscountedPriceExpression =
            x => x.Price - x.Price / 100 * x.DiscountPercent;

        public static Expression<Func<Product, Product>> UpdatePurchaseCountExpression(int count) =>
            product => new Product()
            {
                PurchaseCount = product.PurchaseCount + count
            };

        public static readonly ProductSpecs Specs = new ProductSpecs();

        protected Product()
        {
        }

        public Product(Category category, string name, double price, int discountPercent)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
            DateCreated = DateTime.UtcNow;
            this.EnsureInvariant();
        }

        [Required] public string Name { get; protected set; }

        public double Price { get; protected set; }

        public int DiscountPercent { get; protected set; }

        public DateTime DateCreated { get; protected set; }

        public virtual Category Category { get; protected set; }

        public int PurchaseCount { get; set; }

        public double GetDiscountedPrice()
        {
            return DiscountedPriceExpression.AsFunc()(this);
        }
    }
}