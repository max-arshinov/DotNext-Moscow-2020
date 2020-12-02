using System;
using System.ComponentModel.DataAnnotations;
using Force.Ddd.DomainEvents;
using Infrastructure.Ddd;

namespace Infrastructure.Examples.Domain.Entities
{
    public class AuditLog: IntEntityBase, IDomainEvent
    {
        [Required]
        public string EventName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public int? EntityId { get; set; }

        public override string ToString() =>
            $"{UserName} / {EventName} / {EntityId}";

        public DateTime Happened { get; } = DateTime.UtcNow;
    }
}