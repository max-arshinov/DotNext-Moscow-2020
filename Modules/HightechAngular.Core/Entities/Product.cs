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

        public static readonly ProductSpecs Specs = new ProductSpecs();

        public Product()
        {
        }

        public Product(Category category, string name, double price, int discountPercent, DateTime? created = null)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
            Created = created ?? DateTime.UtcNow;
            this.EnsureInvariant();
        }

        [Required] 
        public string Name { get; set; }

        public double Price { get; set; }

        public int DiscountPercent { get; set; }

        public DateTime Created { get; set; }

        public virtual Category Category { get; set; }

        public int PurchaseCount { get; set; }

        public double GetDiscountedPrice()
        {
            return DiscountedPriceExpression.AsFunc()(this);
        }
    }
}