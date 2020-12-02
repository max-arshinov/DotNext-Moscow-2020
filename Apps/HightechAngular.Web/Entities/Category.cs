using System.ComponentModel.DataAnnotations;
using Force.Extensions;
using Infrastructure.Ddd;

namespace HightechAngular.Orders.Entities
{
    public class Category : IntEntityBase
    {
        public Category(string name)
        {
            Name = name;
            this.EnsureInvariant();
        }

        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}