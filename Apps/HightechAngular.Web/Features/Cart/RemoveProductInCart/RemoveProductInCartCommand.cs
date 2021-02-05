using Force.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Cart
{
    public class RemoveProductInCartCommand : ICommand<bool>
    {
        public int ProductId { get; set; }
        public RemoveProductInCartCommand(int productId) =>
            ProductId = productId;
    }
}
