using Infrastructure.Cqrs;
using Infrastructure.Examples.Domain.Entities;

namespace Infrastructure.Examples.Domain.Features
{
    public class UpdateProductHandler: UpdateIntEntityHandlerBase<Product, UpdateProduct>
    {
        protected override void UpdateEntity(Product entity, UpdateProduct command)
        {
            entity.Update(command);
        }
    }
}