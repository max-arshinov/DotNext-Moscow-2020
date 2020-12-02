using Infrastructure.Cqrs;
using Infrastructure.Examples.Domain.Entities;

namespace Infrastructure.Examples.Domain.Features
{
    public class DeleteProductHandler: DeleteIntEntityHandlerBase<Product, DeleteProduct>
    {
    }
}