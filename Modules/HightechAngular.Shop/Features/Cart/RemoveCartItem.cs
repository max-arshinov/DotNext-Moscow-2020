using System.ComponentModel.DataAnnotations;
using Force.Cqrs;
using Force.Ddd;

namespace HightechAngular.Shop.Features.Cart
{
    public class RemoveCartItem : HasIdBase, ICommand<bool>
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; }

        public RemoveCartItem(int productId)
        {
            ProductId = productId;
        }
    }
}