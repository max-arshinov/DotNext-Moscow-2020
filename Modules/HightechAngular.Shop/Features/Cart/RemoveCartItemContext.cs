using System.ComponentModel.DataAnnotations;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.OperationContext;

namespace HightechAngular.Shop.Features.Cart
{
    public class RemoveCartItemContext : OperationContextBase<RemoveCartItem>, ICommand<bool>
    {
        [Required]
        public Product Product { get; }
        
        public RemoveCartItemContext(RemoveCartItem request, Product product) : base(request)
        {
            Product = product;
        }
    }
}