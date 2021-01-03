using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Force.Expressions;
using Force.Extensions;
using Infrastructure.Ddd;

namespace HightechAngular.Orders.Entities
{
    public class Product : IntEntityBase
    {
        public static readonly Expression<Func<Product, double>> DiscountedPriceExpression =
            x => x.Price - x.Price / 100 * x.DiscountPercent;

        public static readonly ProductSpecs Specs = new ProductSpecs();

        // ReSharper disable VirtualMemberCallInConstructor
        protected Product()
        {
            Name = default!;
            Category = default!;
        }

        public Product(Category category, string name, double price, int discountPercent, 
            DateTime? created = default)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
            Created = created ?? DateTime.UtcNow;
            this.EnsureInvariant();
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
        [Required]
        public string Name { get; protected set; }

        public double Price { get; protected set; }

        public int DiscountPercent { get; protected set; }

        public DateTime Created { get; protected set; }

        public virtual Category Category { get; protected set; }

        public int PurchaseCount { get; set; }
        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Global

        public static Expression<Func<Product, Product>> UpdatePurchaseCountExpression(int count)
        {
            return product => new Product
            {
                PurchaseCount = product.PurchaseCount + count
            };
        }

        public double GetDiscountedPrice()
        {
            return DiscountedPriceExpression.AsFunc()(this);
        }
    }
}