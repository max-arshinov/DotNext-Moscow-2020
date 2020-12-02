using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Force.Ddd;
using Force.Ddd.DomainEvents;
using Infrastructure.Ddd;
using Infrastructure.Examples.Domain.Features;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Examples.Domain.Entities
{
    public partial class Product: IntEntityBase, IHasDomainEvents
    {
        public static ProductSpecs Specs = new ProductSpecs();
        
        private DomainEventStore _domainEventStore = new DomainEventStore();
        
        // EF Only
        protected Product(){}
        
        public Product(CreateProduct command)
        {
            Name = command.Name;
            Price = command.Price;
        }

        [Required, StringLength(Strings.DefaultLength)]
        public string Name { get; protected set; }
        
        [Range(0, 1000000)]
        public double Price { get; protected set; }
        
        [Range(0, 100)]
        public int DiscountPercent { get; protected set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;

        public void Update(UpdateProduct command, [CanBeNull]IdentityUser user = null)
        {
            Name = command.Name;
            Price = command.Price;
            DiscountPercent = command.DiscountPercent;
            _domainEventStore.Raise(new AuditLog
            {
                EntityId = Id,
                EventName = "Product Updated",
                UserName = user?.UserName ?? "Anonymous"
            });
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
            => _domainEventStore;
    }

    public interface IHasDiscount
    {
        int Price { get; set; }
        
        int DiscountPercent { get; set; }

        decimal DiscountedPrice() => Price - Price / 100 * DiscountPercent;
    }
    
}