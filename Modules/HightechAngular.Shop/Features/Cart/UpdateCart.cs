using Force.Cqrs;
using Force.Ddd;

namespace HightechAngular.Shop.Features.Cart
{
    public class UpdateCart : HasIdBase, ICommand
    {
        public UpdateCart(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}