using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.OperationContext;

namespace HightechAngular.Shop.Features.Cart
{
    public class UpdateCartContext : OperationContextBase<UpdateCart>, ICommand
    {
        [Required]
        public Product Product { get; }
        
        public UpdateCartContext(UpdateCart request, Product product) : base(request)
        {
            Product = product;
        }
    }
}